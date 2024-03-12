from functools import singledispatch
from io import StringIO
from typing import Literal


class StringBuilder:
    def __init__(self):
        self.__build = StringIO()

    @property
    def construct(self):
        return self.__build.getvalue()

    @singledispatch
    def add(self, _s: str, before: bool = False) -> Literal[True]:
        if before:
            self.__build.seek(0)
        self.__build.write(_s)
        return True

    @add.register
    def add(self, _s: list, before: bool = False, sep: str = "") -> Literal[True]:
        if before:
            self.__build.seek(0)
        self.__build.write(sep.join(_s))
        return True
