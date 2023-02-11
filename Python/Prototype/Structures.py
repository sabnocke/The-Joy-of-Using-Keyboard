class Node:
    """Generic Node class

    :param data: object to input
    :type data: object

    :param _next: following node
    :type _next: Node or None

    :param _prev: previous node
    :type _prev: Node or None

    """

    def __init__(self, data=None, _next=None, _prev=None):
        self.data: object = data
        self.next: Node | None = _next
        self.prev: Node | None = _prev

    def __call__(self, *args, **kwargs):
        return self(None, None, None)


class StackError(ValueError):
    pass


class StackUnderflow(StackError):
    pass


class StackOverflow(StackError):
    pass


class Stack:
    """Stack implementation

    :raise StackUnderflow: if stack is empty
    """

    def __init__(self):
        self.current = Node()
        self.count: int = 0

    def __repr__(self) -> str:
        return f"top: {self.current.data}"

    def __len__(self):
        return self.count

    def push(self, data):
        new_node = Node(data=data, _next=None, _prev=None)
        if self.current is not None:
            new_node.prev = self.current
            self.current.next = new_node
        self.current = new_node
        self.count += 1

    # noinspection PyUnresolvedReferences
    def pop(self) -> Node:
        """"""
        temp = self.current
        if self.count == 1:
            self.current = None
        if self.count > 1:
            self.current = self.current.prev
        if self.count < 1:
            raise StackUnderflow("Attempted to call pop on empty stack")
        return temp

    def empty(self) -> bool:
        return self.count == 0


class Queue:
    """
    A class to create a queue.
    """

    def __init__(self):
        self.tail = None
        self.head = None
        self.count = 0

    def __repr__(self):
        return f"{self.head.data}"

    def __len__(self):
        """value to return on len() call"""
        return self.count

    def enqueue(self, data: object):
        """
        Adds new element into the queue

        :arg data: object to insert into queue
        :type data: object
        """
        new_node = Node(data=data, _next=None, _prev=None)
        if self.head is None:
            self.head = new_node
            self.tail = self.head
        else:
            new_node.prev = self.tail
            self.tail.next = new_node
            self.tail = new_node
        self.count += 1

    def dequeue(self):
        """Removes appropriate element from queue

        :raise StackUnderflow: if queue is empty
        """
        if self.count == 1:
            self.head = None
            self.tail = None
        elif self.count > 1:
            self.head = self.head.next
            self.head.prev = None
        elif self.count < 1:
            raise StackUnderflow("Attempted to call dequeue on empty queue")
        self.count -= 1



