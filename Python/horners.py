from typing import List
from rnd.ops import compare, unique


def create_mix(one: List[int] | set[int], two: List[int] | set[int]) -> List[int]:
    match compare(len(one), len(two)):
        case 1:
            base, not_base = one, two
        case _:
            base, not_base = two, one
    return [(x * y) for x in base for y in not_base]


def function(nums: List[int] = [], equation: str = ""):
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
    value: int | None = None
    answers = {}
    for item in output:
        placeholder = 0
        mem = []
        for x in nums:
            value = (x + placeholder) * item
            mem.append(x + placeholder)
            placeholder = value
        if value == 0:
            answers[item] = mem

    return answers


ar1 = [2, 7, -15]
ar2 = [1, 1, 2, 2]
print(function(ar1))
print(function(ar2))
