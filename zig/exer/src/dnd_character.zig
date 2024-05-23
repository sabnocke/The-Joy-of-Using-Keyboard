const Random = @import("std").Random;
const std = @import("std");
const testing = std.testing;

pub fn modifier(score: i8) i8 {
    return @divFloor(score - 10, 2);
}

pub fn ability() i8 {
    const rand = std.crypto.random;
    var min: i8 = rand.intRangeAtMost(i8, 1, 6);
    var sum: i8 = 0;
    for (0..2) |_| {
        const one = rand.intRangeAtMost(i8, 1, 6);
        min = @min(one, min);
        sum += one;
    }
    return sum - min;
}

pub const Character = struct {
    strength: i8,
    dexterity: i8,
    constitution: i8,
    intelligence: i8,
    wisdom: i8,
    charisma: i8,
    hitpoints: i8,

    pub fn init() Character {
        const constitution = calculate();
        const hitpoints = 10 + modifier(constitution);
        return Character{ .strength = calculate(), .dexterity = calculate(), .constitution = constitution, .intelligence = calculate(), .wisdom = calculate(), .charisma = calculate(), .hitpoints = hitpoints };
    }

    pub fn calculate() i8 {
        const rand = std.crypto.random;
        var throws = [4]i8{ rand.intRangeAtMost(i8, 1, 6), rand.intRangeAtMost(i8, 1, 6), rand.intRangeAtMost(i8, 1, 6), rand.intRangeAtMost(i8, 1, 6) };
        std.mem.sort(i8, &throws, {}, comptime std.sort.asc(i8));
        var sum: i8 = 0;
        for (0..3) |i| {
            std.debug.print("throws[{}] = {}\n", .{ i, throws[i] });
            sum += throws[i];
        }
        return sum;
    }
};

test "calculate() does something" {
    const actual = Character.calculate();
    try testing.expect(actual >= 3 and actual <= 18);
}

fn isValidAbilityScore(n: isize) bool {
    return n >= 3 and n <= 18;
}
test "random ability is within range" {
    for (0..20) |_| {
        const actual = ability();
        try testing.expect(isValidAbilityScore(actual));
    }
}
pub fn isValid(c: Character) bool {
    return isValidAbilityScore(c.strength) and
        isValidAbilityScore(c.dexterity) and
        isValidAbilityScore(c.constitution) and
        isValidAbilityScore(c.intelligence) and
        isValidAbilityScore(c.wisdom) and
        isValidAbilityScore(c.charisma) and
        (c.hitpoints == 10 + modifier(c.constitution));
}
test "random character is valid" {
    std.debug.print("\n", .{});
    for (0..20) |_| {
        const character = Character.init();
        try testing.expect(isValid(character));
    }
}
