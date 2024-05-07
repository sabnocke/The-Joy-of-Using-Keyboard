import math


def is_armstrong_number(number):
    __sum: int = 0
    index: int = digits(number)
    for digit in split(number):
        __sum += digit**index
    return __sum == number


def split(number: int):
    digit = number
    while digit != 0:
        yield digit % 10
        digit //= 10


def digits(number: int):
    return 1 if number == 0 else int(math.log10(abs(number))) + 1


if __name__ == "__main__":
    print(is_armstrong_number(153))
