#include <stdio.h>
#include "circularBuffer.h"


int main(void) {
  circular_buffer_t *cb = new_circular_buffer(1);
  buffer_value_t item;
  int8_t status = read(cb, &item);
  printf("item: %d, status: %d", item, status);
  perror("");
  // write(cb, 2);
  // iterate(cb);
  printf("Hello World\n");
  delete(cb);
  return 0;
}
