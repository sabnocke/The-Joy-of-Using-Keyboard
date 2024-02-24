Red [
	description: {"Triangle" exercise solution for exercism platform}
	author: "sabnocke"
]

triangle?: func [
    sides[block!]
    return:[logic!]
] [
    a: sides/1
    b: sides/2
    c: sides/3
    to-logic all[
        a > 0 b > 0 c > 0
        a + b >= c b + c >= a a + c >= b
        ]
]

equilateral: function [
	sides[block!]
    return:[logic!]
] [
    a: sides/1
    b: sides/2
    c: sides/3
	to-logic all[(a = b) and (b = c) triangle? sides]
]

isosceles: function [
	sides[block!]
    return:[logic!]
] [
    a: sides/1
    b: sides/2
    c: sides/3
	to-logic all[ any[a = b b = c a = c] triangle? sides ]
]

scalene: function [
	sides
] [
	a: sides/1
    b: sides/2
    c: sides/3
    to-logic all[ a <> b b <> c a <> c triangle? sides]
]

print equilateral [5 5 5]
print isosceles [5 3 2]
print scalene [5 5 5]