import re

roman_numeral_map = (
    ('M', 1000), ('CM', 900),
    ('D', 500), ('CD', 400),
    ('C', 100), ('XC', 90),
    ('L', 50), ('XL', 40),
    ('X', 10), ('IX', 9),
    ('V', 5), ('IV', 4),
    ('I', 1)
)


class OutOfRangeError(ValueError):
    pass


class InputNotSupported(ValueError):
    pass


class InvalidRomanNumeralError(ValueError):
    pass


def to_roman(n):
    """convert int to roman number"""
    if not 0 < n <= 3999:
        raise OutOfRangeError(f'number {n} out of range')
    if type(n) != int:
        raise InputNotSupported(f"input cannot be {type(n)}")

    result: str = ""
    for numeral, integer in roman_numeral_map:
        while n >= integer:
            result += numeral
            n -= integer
    return result


def from_roman(n):
    """convert numeral to integer"""
    roman_numeral_pattern = re.compile("""
        ^                   # start of string
        M{0,3}              # thousands
        (CM|CD|D?C{0,3})    # hundreds
        (XC|XL|L?X{0,3})    # tens
        (IX|IV|V?I{0,3})    # units
        $                   # end of string
    """, re.VERBOSE)
    if not roman_numeral_pattern.search(n):
        raise InvalidRomanNumeralError(f"Invalid roman numeral: {n}")
    result = 0
    index = 0
    for numeral, integer in roman_numeral_map:
        while n[index:index + len(numeral)] == numeral:
            result += integer
            index += len(numeral)
            # print(f"found {numeral} of length {len(numeral)}, adding {integer}")
    return result
