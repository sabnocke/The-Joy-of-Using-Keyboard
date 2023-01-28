import re
import math

def pop_newline(_list: list[str]) -> str | list[str]:
    # __list.reverse()
    for count, item in enumerate(_list):
        if item == '\n':
            _list.pop(count)
    try:
        _list.pop()
    except IndexError:
        return "0"
    return _list


def return_func(func):
    pattern = r"sin\((\d+\/?\d+)\)"
    replacement = r"math.sin(\1)"
    return re.sub(pattern, replacement, func)


def remove_func(func):
    pattern = r"sin\((\d+\/?\d+)\)"
    replacement = r""
    return re.sub(pattern, replacement, func)
