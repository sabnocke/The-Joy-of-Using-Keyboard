const std = @import("std");
const print = @import("std").debug.print;

pub fn primes(buffer: []u32, comptime limit: u32) ![]u32 {
    var isPrime: [limit + 1]bool = undefined;
    for (0..limit+1) |i| {
        isPrime[i] = true;
    }
    var it: u32 = 4;
    var count: u32 = 0;
    while(it <= limit) {
        isPrime[it] = false;
        it += 2;
        count += 1;
    }
    isPrime[0] = false;
    isPrime[1] = false;

    for (2..limit + 1) |i| {
        if(isPrime[i]) {
            var j = i * i;

            while (j < limit + 1) {
                isPrime[j] = false;
                count += 1;
                j = j << 1;
            }
        }
    }

    const length: u32 = limit + 1 - count;

    var j: u32 = 0;
    for (isPrime, 0..) |value, index| {
        if(value) {
            buffer[j] = @intCast(index);
            j += 1;
        }
    }

    return buffer[0..length];
}

pub fn primes2(buffer: []u32, limit: u32) []u32 {
    var j: u32 = 0;
    for(0..limit + 1) |i| {
        const ii: u32 = @intCast(i);
        if(nIsPrime(ii)) {
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