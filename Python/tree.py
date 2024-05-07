from typing import Any, Optional, Never
from dataclasses import dataclass


@dataclass
class Node:
    item: Any = None
    left: Optional["Node"] = None
    right: Optional["Node"] = None
    parent: Optional["Node"] = None


class Tree[T]:
    def __init__(self, item: T) -> None:
        self.root = Node(item, None, None)
        self.size = 0
        self.height = 0

    @staticmethod
    def find_first_empty(n: Node, left: bool) -> Node:
        if left and n.left is not None:
            return Tree.find_first_empty(n.left, left)
        elif n.right is not None:
            return Tree.find_first_empty(n.right, left)
        return n

    def insert(self, value: T, left: bool) -> Never:
        n: Node = self.find_first_empty(self.root, left)
        new: Node = Node(value, None, None)
        if left:
            n.left = new
        else:
            n.right = new

    def insert_rec(self, n: Node, value: T, left: bool) -> Never:
        if left and n.left is not None:
            return self.insert_rec(n.left, value, left)
        elif n.right is not None:
            return self.insert_rec(n.right, value, left)
        new: Node = Node(value, None, None)
        if left:
            n.left = new
        else:
            n.right = new

    def depth_traversal(self, node: Node):
        if node is None:
            return
        yield node.item
        if node.left is not None:
            yield from self.depth_traversal(node.left)
        if node.right is not None:
            yield from self.depth_traversal(node.right)

    @property
    def traverse(self):
        return self.depth_traversal(self.root)


if __name__ == '__main__':
    t = Tree(5)
    t.insert(1, True)
    t.insert(2, True)
    lst = list(t.traverse)
    print(lst)
