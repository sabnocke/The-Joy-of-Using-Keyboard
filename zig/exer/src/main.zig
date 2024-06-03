const std = @import("std");
const assert = @import("std").debug.assert;
const print = @import("std").debug.print;
const linkedList = @import("linkedList.zig");
const sumOfMultiples = @import("sumOfMultiples.zig");

pub fn main() !void {
    print("\n", .{});
    const factors = [_]u32{ 4, 6, 8 };
    const result = try sumOfMultiples.sum(std.heap.page_allocator, &factors, 20);
    print("result = {}\n", .{result});
}
