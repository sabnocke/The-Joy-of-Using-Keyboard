from __future__ import annotations
from typing import Callable
from time import time


class DiscountStrategyValidator:
    @staticmethod
    def validate(obj: Order, value: Callable) -> bool:
        try:
            if obj.price - value(obj) < 0:
                raise ValueError("value cannot be negative")
        except ValueError as exc:
            print(exc)
            return False
        else:
            return True

    def __set_name__(self, owner, name: str) -> None:
        self.private_name = f"_{name}"

    def __set__(self, obj: Order, value: Callable = None) -> None:
        if value and self.validate(obj, value):
            setattr(obj, self.private_name, value)
        else:
            setattr(obj, self.private_name, None)

    def __get__(self, obj: object, objtype: type = None):
        return getattr(obj, self.private_name)


def ten_percent_discount(order: Order) -> float:
    return order.price * .1


def on_sale_discount(order: Order) -> float:
    return order.price * .25 + 20


class Order:
    discount_strategy = DiscountStrategyValidator()

    def __init__(self, price: float, discount_strategy: Callable = None) -> None:
        self.price = price
        self.discount_strategy = discount_strategy

    def apply_discount(self) -> float:
        discount = self.discount_strategy(self) if self.discount_strategy else 0
        return self.price - discount

    def __repr__(self):
        return f"<Order price: {self.price} with discount strategy: {getattr(self.discount_strategy, '__name__', None)}"


def measure(func):
    def wrapper(*args, **kwargs):
        timr = time()
        result = func(*args, **kwargs)
        timr = time() - timr
        print(f"[Finished in {timr}s]")
        return result

    return wrapper


@measure
def main():
    order = Order(100, discount_strategy=ten_percent_discount)
    print(order)
    print(order.apply_discount())


if __name__ == "__main__":
    main()
