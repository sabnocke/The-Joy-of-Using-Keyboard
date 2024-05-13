//
// Created by ReWyn on 13.05.2024.
//

#ifndef ALLYOURBASE_H
#define ALLYOURBASE_H
#include <cstdint>
#include <vector>
using namespace std;

namespace all_your_base {

  template<typename T>
  T intPow(const T x, T p) {
    if (p == 0) return 1;
    if (p == 1) return x;

    const int tmp = intPow<T>(x, p / 2);
    if (p % 2 == 0) return tmp * tmp;
    return x * tmp * tmp;
  }
  vector<unsigned> convert(uint32_t origin, vector<unsigned> &in_digits, uint32_t dest);
}

#endif //ALLYOURBASE_H
