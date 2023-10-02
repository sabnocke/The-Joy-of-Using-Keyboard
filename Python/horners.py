from typing import List
from rnd.ops import compare, unique
import re


def create_mix(one: List[int] | set[int], two: List[int] | set[int]) -> List[int]:
    match compare(len(one), len(two)):
        case 1:
            base, not_base = one, two
        case _:
            base, not_base = two, one
    return [(x * y) for x in base for y in not_base]


def function(nums=None):
    if nums is None:
        nums = []
    upper = nums[len(nums) - 1]
    lower = nums[0]
    upper_divisor = set(
        item for item in range(abs(upper) + 1) if item > 0 and upper % item == 0
    )
    upper_divisor = upper_divisor.union(set(-item for item in upper_divisor))
    lower_divisor = set(
        item for item in range(lower + 1) if item > 0 and lower % item == 0
    )
    output = create_mix(upper_divisor, lower_divisor)
    assert unique(output), "Something has gone wrong."

    return process(output, nums)


def process(insert: List[int], nums):
    value: int | None = None
    answers = {}
    for item in insert:
        placeholder = 0
        mem = []
        for x in nums:
            value = (x + placeholder) * item
            mem.append(x + placeholder)
            placeholder = value
        if value == 0:
            answers[item] = mem

    return answers


actEq = "3x^3+2x^2+5x-7"
ar1 = [2, 7, -15]
ar2 = [1, 1, 2, 2]
# print(function(ar1))
# print(function(ar2))

print(re.findall(r"(?:\wx){1,} (\+ | -)\w", actEq))
