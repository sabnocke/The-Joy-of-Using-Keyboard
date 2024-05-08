from math import sqrt


def heronMethod(n: float | int, precision: int):
    estimate: float = n + 1
    c: float = abs(estimate - n)
    prec: float = 10 ** -precision
    while c >= prec:
        x = 0.5 * (estimate + n / estimate)
        c = abs(estimate - x)
        estimate = x
    return estimate


number: int = 25
print(heronMethod(number, 10))
print(sqrt(number))
