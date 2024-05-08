const std = @import("std");
const print = @import("std").debug.print;

pub fn primes(buffer: []u32, comptime limit: u32) []u32 {
    var stat = std.StaticBitSet(limit + 1).initFull();
    stat.unset(0);
    stat.unset(1);
    var it: u32 = 4;
    while (it < limit + 1) : (it += 2) stat.unset(it);

    var x: u32 = 0;
    for (2..limit + 1) |i| {
        if (!stat.isSet(i)) continue;
        var j = i + i;
        while (j < limit + 1) : (j += i) stat.unset(j);
        buffer[x] = @intCast(i);
        x += 1;
    }

    return buffer[0..x];
}

pub fn primes2(buffer: []u32, limit: u32) []u32 {
    var j: u32 = 0;
    for (0..limit + 1) |i| {
        const ii: u32 = @intCast(i);
        if (nIsPrime(ii)) {
            buffer[j] = ii;
            j += 1;
        }
    }
    return buffer[0..j];
}

pub fn nIsPrime(n: u32) bool {
    if (n == 0) return false;
    if (n == 1) return false;
    const upper: u32 = @intFromFloat(std.math.sqrt(@as(f32, @floatFromInt(n))));
    for (2..upper + 1) |div| {
        if (n % div == 0) return false;
    }
    return true;
}
