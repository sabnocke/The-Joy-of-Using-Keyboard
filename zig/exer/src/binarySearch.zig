const print = @import("std").debug.print;

pub fn binarySearch(comptime T: type, target: T, items: []const T) ?usize {
    if(target < items[0]) return null;

    var first: usize = 0;
    var second: usize = items.len;
    var mid: usize = (first + second) >> 1;

    while(target != items[mid]) {
        if(second - first <= 1) return null;
        if(target > items[mid]) {
            first = mid;
        } else {
            second = mid;
        }
        mid = (first + second) >> 1;
    }
    return mid;
}
