//
// Created by ReWyn on 13.05.2024.
//

#include <iostream>
using namespace std;
#include "all_your_base.h"
#include <cstdint>
#include <vector>
#include <stdexcept>
namespace all_your_base {

  vector<unsigned> convert(const uint32_t origin, vector<unsigned> &in_digits, const uint32_t dest) {
    if (origin < 2) throw std::invalid_argument("input base must be >= 2");
    if (dest < 2) throw std::invalid_argument("output base must be >= 2");
    for(const auto digit : in_digits) {
      if(digit >= origin) throw std::invalid_argument("all digits must satisfy 0 <= d < input base");
    }
    uint32_t sum = 0;
    for(uint32_t i = 0 ; i < in_digits.size() ; ++i) {
      // std::cout << "in_digits = " << in_digits[i] << " pow = " << intPow<uint32_t>(origin, in_digits.size() - i - 1) << std::endl;
      // std::cout << "order" << in_digits.size() - i << std::endl;
      sum += in_digits[i] * intPow<uint32_t>(origin, in_digits.size() - i - 1);
    }
    std::cout << sum << std::endl;
    if (sum == 0) return vector<unsigned>{0};
    auto eax = vector<unsigned>();
    while (sum != 0) {
      eax.insert(eax.begin(), sum % dest);
      sum /= dest;
    }
    return eax;
  }
}  // namespace all_your_base
