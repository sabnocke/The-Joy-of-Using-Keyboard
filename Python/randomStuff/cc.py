from typing import Dict, List, Union, Type
from math import atan, hypot, sin, cos
from sys import float_info

Numeral = Union[int, float]


class RegistryHolder(type):
    REGISTRY: Dict[str, Type["RegistryHolder"] | type] = {}

    def __new__(cls, *args, **kwargs):
        new_class = type.__new__(cls, *args, **kwargs)
        cls.REGISTRY[new_class.__name__] = new_class
        return new_class


class BaseRegistryEntry(metaclass=RegistryHolder):
    """
        Any class that will inherit from BaseRegisteredClass will be included
        inside the dict RegistryHolder. REGISTRY, the key being the name of the
        class and the associated value, the class itself.
        """
    pass


class WrongInputFormatException(Exception):
    pass


def nearlyEqual(a: float, b: float) -> bool:
    abs_a: float = abs(a)
    abs_b: float = abs(b)
    diff: float = abs(a - b)

    if a == b:
        return True
    elif a == 0 or b == 0 or abs_a + abs_b < float_info.min:
        return diff < (float_info.epsilon * float_info.min)
    else:
        return diff / min((abs_a + abs_b), float_info.max) < float_info.epsilon


class CustomComplex(BaseRegistryEntry):
    __slots__ = ("real", "imag")

    def __init__(self, real: Numeral = 0, imag: Numeral = 0):
        self.real, self.imag = real, imag

    def conjugate(self):
        return self.__class__(self.real, -self.imag)

    def argz(self):
        return atan(self.imag / self.real)

    def __abs__(self):
        return hypot(self.real, self.imag)

    def __repr__(self):
        return f"{self.__class__.__name__}({self.real}, {self.imag})"

    def __str__(self):
        return f"({self.real}+{self.imag}i)"

    def __add__(self, other):
        if isinstance(other, Numeral):
            return self.__class__(self.real + other, self.imag)

        if not isinstance(other, CustomComplex):
            raise WrongInputFormatException(f"{self.__class__.__name__} "
                                            f"does not support addition of self and {type(other)}")

        return self.__class__(self.real + other.real, self.imag + other.imag)

    def __sub__(self, other):
        if isinstance(other, Numeral):
            return self.__class__(self.real - other, self.imag)

        if not isinstance(other, CustomComplex):
            raise WrongInputFormatException(f"{self.__class__.__name__} "
                                            f"does not support addition of self and {type(other)}")

        return self.__class__(self.real - other.real, self.imag - other.imag)

    def __mul__(self, other):
        if isinstance(other, Numeral):
            return self.__class__(self.real * other, self.imag * other)

        if not isinstance(other, CustomComplex):
            raise WrongInputFormatException(f"{self.__class__.__name__} "
                                            f"does not support addition of self and {type(other)}")

        return self.__class__(
            (self.real * other.real) - (self.imag * other.imag),
            (self.real * self.imag) - (self.real * other.imag)
        )

    def __radd__(self, other):
        return self.__add__(other)

    def __rmul__(self, other):
        return self.__mul__(other)

    def __rsub__(self, other):
        if isinstance(other, Numeral):
            return self.__class__(other - self.real, -self.imag)
        else:
            raise NotImplementedError

    def __eq__(self, other) -> bool:
        if self.type_check(other, float):
            return nearlyEqual(self.real, other.real) and nearlyEqual(self.imag, other.imag)
        else:
            return self.real == other.real and self.imag == other.imag

    def type_check(self, /, obj, _type: type, ) -> bool:
        p1: bool = isinstance(self.real, _type) and isinstance(obj.real, _type)
        p2: bool = isinstance(self.imag, _type) and isinstance(obj.imag, _type)
        return p1 or p2

    def __ne__(self, other):
        return (self.real != other.real) or (self.imag != other.imag)

    def __pow__(self, power, modulo=None):
        r_raised = abs(self) ** power
        argz_mul = self.argz() * power

        real_part = round(r_raised * cos(argz_mul))
        imag_part = round(r_raised * sin(argz_mul))

        return self.__class__(real_part, imag_part)


def main() -> None:
    z1 = CustomComplex(8, 8)
    z2 = CustomComplex(5, 10)
    print(z1 + 8)

    print(RegistryHolder.REGISTRY)


if __name__ == "__main__":
    main()
