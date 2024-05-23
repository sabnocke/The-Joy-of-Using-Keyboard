const std = @import("std");
const assert = @import("std").debug.assert;
const leap = @import("leap.zig");
const sieve = @import("sieve.zig");
const print = @import("std").debug.print;
const collatz = @import("collatz.zig");
const bins = @import("binarySearch.zig");
const scrabble = @import("scrabble.zig");
const pangram = @import("pangram.zig");
const armstrong = @import("armstrongNumbers.zig");
const raindrops = @import("raindrops.zig");
const grains = @import("grains.zig");
const perfectNumbers = @import("perfectNumbers.zig");
const triangle = @import("triangle.zig");
const isogram = @import("isogram.zig");
const testing = @import("std").testing;
const dnd = @import("dnd_character.zig");

pub fn main() !void {
    print("\n", .{});
    // var buffer: [15]u8 = undefined;
    // const response = try collatz.steps(1000000);
    // print("\nresponse = {}\n", .{response});
    // print("{}\n", .{'a' * 5});
    // const array = [_]i32{ 1, 3, 4, 6, 8, 9, 11 };
    // const find: usize = 6;
    const character = dnd.Character.init();
    const response = dnd.isValid(character);
    print("response = {}\n", .{response});
    print("attrs:\n", .{});
    print("{}\n", .{character.charisma});
    print("{}\n", .{character.dexterity});
    print("c = {}\n", .{character.constitution});
    print("{}\n", .{character.intelligence});
    print("{}\n", .{character.wisdom});
    print("h = {}\n", .{character.hitpoints});
    print("mod = {}", .{dnd.modifier(character.constitution)});
}
