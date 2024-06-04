const std = @import("std");

pub fn isValid(s: []const u8) bool {
    _ = s;
    const allocator = std.heap.GeneralPurposeAllocator(.{}).allocator();
    const number = try allocator.create(u32);
    defer number;
    std.debug.print("{}", .{number});
    return true;
}
