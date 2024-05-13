//
// Created by ReWyn on 13.05.2024.
//

/*
from typing import Never, Callable, Literal, Union, AnyStr
class BufferFullException(BufferError):
    """Exception raised when CircularBuffer is full.

    message: explanation of the error.

    """

    def __init__(self, message):
        self.message = message
class BufferEmptyException(BufferError):
    """Exception raised when CircularBuffer is empty.

    message: explanation of the error.

    """

    def __init__(self, message):
        self.message = message
class CircularBuffer:
    def __init__(self, capacity) -> None:
        self.capacity: int = capacity
        self.buffer: list[Union[Literal[0], AnyStr]] = [0] * capacity
        self.position: int = 0
        self.size: Callable[[], int] = lambda: self.capacity - self.buffer.count(0)
        self.alignPosition: Callable[[int], int] = lambda pos: (pos + 1) % self.capacity
        self.delete_position: int = 0

    def read(self) -> AnyStr:
        if all(i == 0 for i in self.buffer):
            raise BufferEmptyException("Circular buffer is empty")
        item = self.buffer[self.delete_position]
        self.buffer[self.delete_position] = 0
        self.delete_position = self.alignPosition(self.delete_position)
        return item

    def write(self, data: AnyStr):
        if self.position >= self.capacity or self.size() >= self.capacity:
            raise BufferFullException('Circular buffer is full')
        self.buffer[self.position] = data
        self.position = self.alignPosition(self.position)

    def overwrite(self, data: AnyStr) -> Never:
        if self.capacity > self.size():
            self.write(data)
            return
        self.buffer[self.delete_position] = data
        self.delete_position = self.alignPosition(self.delete_position)

    def clear(self) -> Never:
        self.buffer: list = [0] * self.capacity
        self.delete_position = self.position = 0
 */

#ifndef CIRCULARBUFFER_H
#define CIRCULARBUFFER_H
#include <stdint.h>



typedef int8_t failure_t;
typedef int32_t buffer_value_t;
typedef struct circular_buffer {
    uint32_t capacity;
    buffer_value_t *buffer;
    uint32_t position;
    uint32_t delete_position;
} circular_buffer_t;

failure_t init(circular_buffer_t *cb, const uint32_t cap);
void dest(circular_buffer_t *cb);
uint32_t is_empty(const circular_buffer_t *cb);
failure_t read(circular_buffer_t *cb, buffer_value_t *dest);
failure_t write(circular_buffer_t *cb, const buffer_value_t src);
failure_t overwrite(circular_buffer_t *cb, const buffer_value_t src);
void clear(const circular_buffer_t *cb);
void iterate(const circular_buffer_t *cb);
circular_buffer_t *new_circular_buffer(const uint32_t cap);
void delete(circular_buffer_t *cb_ptr);
#endif //CIRCULARBUFFER_H
