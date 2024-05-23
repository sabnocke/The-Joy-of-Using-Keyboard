// I don't know enough to complete this...

const std = @import("std");
const expect = std.testing.expect;

pub fn LinkedList(comptime T: type) type {
    return struct {
        // Please implement the doubly linked `Node` (replacing each `void`).
        pub const Node = struct {
            prev: ?*Node = null,
            next: ?*Node = null,
            data: ?T = null,
        };

        // Please implement the fields of the linked list (replacing each `void`).
        first: ?Node,
        // last: Node = Node{},
        len: usize = 0,

        const Self = @This();

        // Please implement the below methods.
        // You need to add the parameters to each method.

        pub fn push(self: Self, src: *Node) void {
            if (self.first.next == null) {
                // Sets given node as both first and last (it's the only one)
                self.first = src;
                self.last = src;
                self.len += 1;
            } else {
                self.first.next = src;
                self.last = src;
                self.len += 1;
            }
        }

        pub fn pop() void {
            // Please implement this method.
            // It must return an optional pointer to a Node.
        }

        pub fn shift() void {
            // Please implement this method.
            // It must return an optional pointer to a Node.
        }

        pub fn unshift() void {
            // Please implement this method.
        }

        pub fn delete() void {
            // Please implement this method.
            // It must modify the list only when it contains the given node.
        }
    };
}

test "Push does what push is supposed to do" {
    const List = LinkedList(i32);
    var a = List.Node{ .data = 10 };
    var list = List{};
    list.push(&a);
    try expect(list.first.data == a.data);
}
