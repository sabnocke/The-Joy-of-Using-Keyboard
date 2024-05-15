const std = @import("std");
const ChessboardError = error {
    IndexOutOfBounds
};

pub fn square(index: usize) ChessboardError!u64 {
    if (index > 64 or index == 0) return ChessboardError.IndexOutOfBounds;
    return std.math.pow(u64, 2, index - 1);
}

pub fn total() u128 {
    return std.math.pow(u128, 2, 64) - 1;
}
