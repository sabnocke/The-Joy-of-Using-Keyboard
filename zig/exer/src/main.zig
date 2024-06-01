const std = @import("std");
const assert = @import("std").debug.assert;
const print = @import("std").debug.print;
const linkedList = @import("linkedList.zig");

pub fn main() !void {
    print("\n", .{});

    const List = linkedList.LinkedList(i32);
    var list = List{};

    var one = List.Node{ .data = 1 };
    var two = List.Node{ .data = 2 };
    var three = List.Node{ .data = 3 };
    var four = List.Node{ .data = 4 };
    var five = List.Node{ .data = 5 };

    list.push(&one);
    list.push(&two);
    list.push(&three);
    list.push(&four);
    list.push(&five);

    var t = list.first;
    print("len = {}\n", .{list.len});
    while (t) |node| : (t = node.next) {
        print("value = {}\n", .{node.data});
    }
    list.unshift(&five);
    t = list.first;
    print("unshift value = {}\n", .{t.?.data});
    print("len = {}\n", .{list.len});
}
