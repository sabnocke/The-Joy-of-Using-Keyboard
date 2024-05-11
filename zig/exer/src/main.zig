const std = @import("std");
const assert = @import("std").debug.assert;
const leap = @import("leap.zig");
const sieve = @import("sieve.zig");
const print = @import("std").debug.print;
const collatz = @import("collatz.zig");
const bins = @import("binarySearch.zig");
const scrabble = @import("scrabble.zig");

pub fn main() !void {
    print("\n", .{});
    // const response = try collatz.steps(1000000);
    // print("\nresponse = {}\n", .{response});
    // print("{}\n", .{'a' * 5});
    // const array = [_]i32{ 1, 3, 4, 6, 8, 9, 11 };
    // const find: usize = 6;
    const response = scrabble.score("cabbage");
    print("\nresponse = {?}\n", .{response});
}

test "simple test" {
    var list = std.ArrayList(i32).init(std.testing.allocator);
    defer list.deinit(); // try commenting this out and see if zig detects the memory leak!
    try list.append(42);
    try std.testing.expectEqual(@as(i32, 42), list.pop());
}
