const std = @import("std");

pub const Classification = enum {
    deficient,
    perfect,
    abundant,
    };

pub fn classify(n: u64) Classification {
    var sum: u64 = 0;
    for (1..n) |i| { sum += if (n % i == 0) i else 0; }
    if (n > sum) return .deficient;
    if (n < sum) return .abundant;
    return .perfect;
}
