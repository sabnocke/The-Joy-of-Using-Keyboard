const std = @import("std");
const mem = std.mem;
const testing = std.testing;
const thread = std.Thread;

pub fn sum(_: mem.Allocator, factors: []const u32, limit: u32) !u64 {
    var eax: u64 = 0;
    for (0..factors.len) |index| {
        eax += findMultiplesLimited(factors[0..index], factors[index], limit);
    }
    return eax;
}

pub fn findMultiplesLimited(factors: []const u32, number: u32, limit: u32) u64 {
    var solution: u64 = 0;
    var eax: u64 = number;
    if (eax == 0) return solution;
    // there is no need to calculate multiple of previously seen factor
    if (!isCorrectValue(factors, number)) return solution;
    while (eax < limit) : (eax += number) {
        if (isCorrectValue(factors, eax)) solution += eax;
    }
    return solution;
}

/// Checks if a number is divisible by one of the factors
fn isCorrectValue(factors: []const u32, number: u64) bool {
    var ecx: u32 = 0;
    for (factors) |factor| {
        if (number % factor != 0) ecx += 1;
    }
    return ecx == factors.len;
}

pub fn sumParallel(allocator: mem.Allocator, factors: []const u32, limit: u32) !u64 {
    var eax = std.ArrayList(u64).init(allocator);
    defer eax.deinit();
    var threads = std.ArrayList(std.Thread).init(allocator);
    defer threads.deinit();
    for (0..factors.len) |index| {
        const t = try thread.spawn(.{}, findMultiplesLimitedParallel, .{ factors[0..index], factors[index], limit, &eax });
        try threads.append(t);
    }
    for (threads.items) |t| t.join();

    var s: u64 = 0;
    for (eax.items) |item| s += item;
    return s;
}

fn findMultiplesLimitedParallel(factors: []const u32, number: u32, limit: u32, q: anytype) !void {
    var solution: u64 = 0;
    var eax: u64 = number;
    if (eax == 0) return;
    // there is no need to calculate multiple of previously seen factor
    if (!isCorrectValue(factors, number)) return;
    while (eax < limit) : (eax += number) {
        if (isCorrectValue(factors, eax)) solution += eax;
    }
    try q.append(solution);
}

test "solutions using include-exclude must extend to cardinality greater than 3" {
    const expected: u64 = 39_614_537;
    const factors = [_]u32{ 2, 3, 5, 7, 11 };
    const limit = 10_000;
    const actual = try sum(testing.allocator, &factors, limit);
    try testing.expectEqual(expected, actual);
}

test "sum is greater than maximum value of u32" {
    // Note that for a u32 `limit`, the maximum sum of multiples fits in a u64.
    const expected: u64 = 4_500_000_000;
    const factors = [_]u32{100_000_000};
    const limit = 1_000_000_000;
    const actual = try sum(testing.allocator, &factors, limit);
    try testing.expectEqual(expected, actual);
}
test "[Parallel] solutions using include-exclude must extend to cardinality greater than 3" {
    const expected: u64 = 39_614_537;
    const factors = [_]u32{ 2, 3, 5, 7, 11 };
    const limit = 10_000;
    const actual = try sumParallel(testing.allocator, &factors, limit);
    try testing.expectEqual(expected, actual);
}

test "[Parallel] sum is greater than maximum value of u32" {
    // Note that for a u32 `limit`, the maximum sum of multiples fits in a u64.
    const expected: u64 = 4_500_000_000;
    const factors = [_]u32{100_000_000};
    const limit = 1_000_000_000;
    const actual = try sumParallel(testing.allocator, &factors, limit);
    try testing.expectEqual(expected, actual);
}
