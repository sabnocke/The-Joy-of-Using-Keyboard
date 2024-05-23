const std = @import("std");

pub fn isIsogram(str: []const u8) bool {
    var map = std.bit_set.IntegerBitSet(26).initEmpty();
    for (str) |char| {
        if (!std.ascii.isAlphabetic(char)) continue;
        if (map.isSet(std.ascii.toLower(char) - 'a')) return false;
        map.set(std.ascii.toLower(char) - 'a');
    }
    return true;
}
