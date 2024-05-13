const std = @import("std");
const mem = std.mem;

pub fn convert(buffer: [] u8, n: u32) []u8 {
    var b = std.io.fixedBufferStream(buffer);
    if (n % 3 == 0) b.writer().writeAll("Pling") catch unreachable;
    if (n % 5 == 0) b.writer().writeAll("Plang") catch unreachable;
    if (n % 5 == 0) b.writer().writeAll("Plong") catch unreachable;
    if (b.pos == 0) b.writer().print("{d}", .{n}) catch unreachable;
    return b.getWritten();
}

fn sign(n: u32) u32 {
    if (n == 0) return 1;
    return 0;
}

fn intToString(int: u32, buf: []u8) ![]u8 {
    return try std.fmt.bufPrint(buf, "{}\xaa", .{int});
}