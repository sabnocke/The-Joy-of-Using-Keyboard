const std = @import("std");
const expect = std.testing.expect;
const expectEqual = std.testing.expectEqual;

pub fn LinkedList(comptime T: type) type {
    return struct {
        const Self = @This();

        pub const Node = struct {
            prev: ?*Node = null,
            next: ?*Node = null,
            data: T,
        };

        first: ?*Node = null,
        last: ?*Node = null,
        len: usize = 0,

        fn insertAfter(self: *Self, node: *Node, new_node: *Node) void {
            new_node.prev = node;
            if (node.next) |next_node| {
                new_node.next = next_node;
                next_node.prev = new_node;
            } else {
                new_node.next = null;
                self.last = new_node;
            }
            node.next = new_node;
            self.len += 1;
        }

        fn insertBefore(self: *Self, node: *Node, new_node: *Node) void {
            new_node.next = node;
            if (node.prev) |prev| {
                new_node.prev = prev;
                prev.next = new_node;
            } else {
                new_node.prev = null;
                self.first = new_node;
            }
            node.prev = new_node;
            self.len += 1;
        }

        pub fn push(self: *Self, new_node: *Node) void {
            if (self.last) |last| {
                self.insertAfter(last, new_node);
            } else {
                self.first = new_node;
                self.last = new_node;
                new_node.next = null;
                new_node.prev = null;

                self.len = 1;
            }
        }

        fn remove(self: *Self, node: *Node) void {
            if (node.prev) |prev| {
                prev.next = node.next;
            } else {
                self.first = node.next;
            }

            if (node.next) |next| {
                next.prev = node.prev;
            } else {
                self.last = node.prev;
            }
            self.len -= 1;
        }

        pub fn pop(self: *Self) ?*Node {
            const last = self.last orelse return null;
            self.remove(last);
            return last;
        }

        pub fn shift(self: *Self) ?*Node {
            const first = self.first orelse return null;
            self.remove(first);
            return first;
        }

        pub fn unshift(self: *Self, new_node: *Node) void {
            if (self.first) |first| {
                self.insertBefore(first, new_node);
            } else {
                self.first = new_node;
                self.last = new_node;
                new_node.next = null;
                new_node.prev = null;

                self.len = 1;
            }
        }

        pub fn delete(self: *Self, node: *Node) void {
            var t = self.first;
            while (t) |iter| : (t = iter.next) {
                if (iter.data == node.data) {
                    self.remove(node);
                    break;
                }
            }
        }
    };
}

test "Linked list push works..." {
    const List = LinkedList(i32);
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
    try expect(list.len == 5);
    {
        var t = list.first;
        var index: u32 = 1;
        while (t) |node| : (t = node.next) {
            try expect(node.data == index);
            index += 1;
        }
    }
}

test "shift gets an element from the list" {
    const List = LinkedList(u32);
    var list = List{};
    var a = List.Node{ .data = 17 };
    list.push(&a);
    try expectEqual(@as(usize, 17), list.shift().?.data);
}
test "shift gets first element from the list" {
    const List = LinkedList(u32);
    var list = List{};
    var a = List.Node{ .data = 23 };
    var b = List.Node{ .data = 5 };
    list.push(&a);
    list.push(&b);
    try expectEqual(@as(usize, 23), list.shift().?.data);
    try expectEqual(@as(usize, 5), list.shift().?.data);
}

test "unshift adds element at start of the list" {
    const List = LinkedList(u32);
    var list = List{};
    var a = List.Node{ .data = 23 };
    var b = List.Node{ .data = 5 };
    list.unshift(&a);
    list.unshift(&b);
    try expectEqual(@as(usize, 5), list.shift().?.data);
    try expectEqual(@as(usize, 23), list.shift().?.data);
}

test "pop, push, shift, and unshift can be used in any order" {
    const List = LinkedList(u32);
    var list = List{};
    var a = List.Node{ .data = 1 };
    var b = List.Node{ .data = 2 };
    var c = List.Node{ .data = 3 };
    var d = List.Node{ .data = 4 };
    var e = List.Node{ .data = 5 };
    list.push(&a);
    list.push(&b);
    try expectEqual(@as(usize, 2), list.pop().?.data);
    list.push(&c);
    try expectEqual(@as(usize, 1), list.shift().?.data);
    list.unshift(&d);
    list.push(&e);
    try expectEqual(@as(usize, 4), list.shift().?.data);
    try expectEqual(@as(usize, 5), list.pop().?.data);
    try expectEqual(@as(usize, 3), list.shift().?.data);
}

test "count is correct after mutation" {
    const List = LinkedList(u32);
    var list = List{};
    var a = List.Node{ .data = 31 };
    var b = List.Node{ .data = 43 };
    list.push(&a);
    try expectEqual(@as(usize, 1), list.len);
    list.unshift(&b);
    try expectEqual(@as(usize, 2), list.len);
    _ = list.shift();
    try expectEqual(@as(usize, 1), list.len);
    _ = list.pop();
    try expectEqual(@as(usize, 0), list.len);
}
test "deletes the element with the specified value from the list" {
    const List = LinkedList(u32);
    var list = List{};
    var a = List.Node{ .data = 71 };
    var b = List.Node{ .data = 83 };
    var c = List.Node{ .data = 79 };
    list.push(&a);
    list.push(&b);
    list.push(&c);
    list.delete(&b);
    try expectEqual(@as(usize, 2), list.len);
    try expectEqual(@as(usize, 79), list.pop().?.data);
    try expectEqual(@as(usize, 71), list.shift().?.data);
}
test "deletes only the first occurrence" {
    const List = LinkedList(u32);
    var list = List{};
    var a = List.Node{ .data = 73 };
    var b = List.Node{ .data = 9 };
    var c = List.Node{ .data = 9 };
    var d = List.Node{ .data = 107 };
    list.push(&a);
    list.push(&b);
    list.push(&c);
    list.push(&d);
    list.delete(&b);
    try expectEqual(@as(usize, 3), list.len);
    try expectEqual(@as(usize, 107), list.pop().?.data);
    try expectEqual(@as(usize, 9), list.pop().?.data);
    try expectEqual(@as(usize, 73), list.pop().?.data);
}
