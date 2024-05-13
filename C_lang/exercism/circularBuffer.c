//
// Created by ReWyn on 13.05.2024.
//

#include "circularBuffer.h"

#include <stdio.h>
#include <stdlib.h>
#include <errno.h>
#define align(pos) (pos = (pos + 1) % cb->capacity)

failure_t init(circular_buffer_t *cb, const uint32_t cap) {
  cb->capacity = cap;
  cb->buffer = (buffer_value_t *)calloc(sizeof(buffer_value_t), sizeof(buffer_value_t) * cap);
  // cb->buffer = (buffer_value_t *)malloc(sizeof(buffer_value_t) * cap);
  if (cb->buffer == NULL) return -1;
  cb->delete_position = 0;
  cb->position = 0;
  return 0;
}
void dest(circular_buffer_t *cb) {
  if (cb->buffer != NULL) {
    free(cb->buffer);
    cb->buffer = NULL;
  }
  cb->capacity = 0;
  cb->delete_position = 0;
  cb->position = 0;
}

uint32_t is_empty(const circular_buffer_t *cb) {
  unsigned zeros = 0;
  for (uint32_t i = 0; i < cb->capacity; ++i) {
    if(cb->buffer[i] == 0) zeros++;
  }
  return zeros;
}

failure_t read(circular_buffer_t *cb, buffer_value_t *dest) {
  const unsigned size = cb->capacity - is_empty(cb);
  if(size == 0) {
    errno = 61;
    return 1;
  }
  const buffer_value_t item = cb->buffer[cb->delete_position];
  cb->buffer[cb->delete_position] = 0;
  align(cb->delete_position);
  *dest = item;
  return 0;
}

failure_t write(circular_buffer_t *cb, const buffer_value_t src) {
  const unsigned size = cb->capacity - is_empty(cb);
  if(cb->position >= cb->capacity || size >= cb->capacity) {
    errno = 105;
    return 1;
  }
  cb->buffer[cb->position] = src;
  align(cb->position);
  return 0;
}

failure_t overwrite(circular_buffer_t *cb, const buffer_value_t src) {
  const unsigned size = cb->capacity - is_empty(cb);
  if (cb->capacity > size) {return write(cb, src);}
  cb->buffer[cb->delete_position] = src;
  align(cb->delete_position);
  return 0;
}

void clear(const circular_buffer_t *cb) {
  for(uint32_t i = 0; i < cb->capacity; ++i) {
    cb->buffer[i] = 0;
  }
}

void iterate(const circular_buffer_t *cb) {
  for(uint32_t i = 0; i < cb->capacity; ++i) {
    printf("[%u: %d]", i, cb->buffer[i]);
  }
  printf("\n");
}

circular_buffer_t *new_circular_buffer(const uint32_t cap) {
  circular_buffer_t *cb_ptr = (circular_buffer_t *)malloc(sizeof(circular_buffer_t));
  if(cb_ptr == NULL) return NULL;
  init(cb_ptr, cap);
  return cb_ptr;
}


void delete(circular_buffer_t *cb_ptr) {
  dest(cb_ptr);
  free(cb_ptr);
  cb_ptr = NULL;
}