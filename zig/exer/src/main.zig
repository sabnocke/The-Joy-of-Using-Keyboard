const std = @import("std");
const assert = @import("std").debug.assert;
const print = @import("std").debug.print;
const luhn = @import("luhn.zig");

pub fn main() !void {
    print("\n", .{});
    const result = luhn.isValid("1568");
    print("result = {any}\n", .{result});
}
