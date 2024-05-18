#include <iostream>
#include "all_your_base.h"
using namespace std;
int main() {
  std::cout << "Hello, World!" << std::endl;
  auto in = vector<unsigned>{1, 0, 1, 0, 1, 0};
  auto out = all_your_base::convert(2, in, 10);
  for (const auto digit : out) {
    std::cout << digit << " ";
  }
  return 0;
}
