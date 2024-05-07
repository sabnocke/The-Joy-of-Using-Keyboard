const std = @import("std");
const assert = @import("std").debug.assert;
const leap = @import("leap.zig");
const sieve = @import("sieve.zig");
const print = @import("std").debug.print;

pub fn main() !void {

    const year: u32 = 2020;
    const number: bool = leap.leap_year(year);
    var buffer: [168]u32 = undefined;
    std.debug.print("is_leap?: {} = {}\n", .{year, number});
    const limit: u32 = 10;
    //const response = try sieve.primes(&buffer, limit);
    //_ = response;
    // try sieve.primes(limit);
    //std.debug.print("response: {any}\n", .{response});
    const response = sieve.primes2(&buffer, limit);
    print("response = {any}", .{response});
    const b: bool = sieve.nIsPrime(6);
    print("{}\n", .{b});
}

test "simple test" {
    var list = std.ArrayList(i32).init(std.testing.allocator);
    defer list.deinit(); // try commenting this out and see if zig detects the memory leak!
    try list.append(42);
    try std.testing.expectEqual(@as(i32, 42), list.pop());
}
