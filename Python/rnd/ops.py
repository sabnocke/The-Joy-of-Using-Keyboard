from typing import Union
from array import array
from collections import deque

iterable = Union[list, tuple, set, dict, array, deque]

def compare(one: int, two: int) -> int: # type: ignore
    if one >= two:
        return 1
    elif one <= two:
        return -1
    elif one == two:
        return 0
    
def divide(one: int, two: int):
    return one % two == 0

def unique(tst: iterable) -> bool:
    return len(tst) == len(set(tst))