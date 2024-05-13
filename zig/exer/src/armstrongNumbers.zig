const std = @import("std");

pub fn isArmstrongNumber(num: u128) bool {
    const digits: u16 = std.math.log10_int(num) + 1;
    var sum: u128 = 0;
    var n: u128 = num;
    while(n != 0) : (n /= 10) {
        sum += std.math.pow(u128, n % 10, digits);
    }
    return sum == num;
}

