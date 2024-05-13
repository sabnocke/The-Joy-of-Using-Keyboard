const std = @import("std");
const print = @import("std").debug.print;

pub fn is_pangram(string: []const u8) bool {
    var map = std.bit_set.IntegerBitSet(26).initEmpty();
    for (string) |char| {
        if (!std.ascii.isAlphabetic(char)) continue;
        map.set(std.ascii.toLower(char) - 'a');
    }
    return map.count() == 26;
}