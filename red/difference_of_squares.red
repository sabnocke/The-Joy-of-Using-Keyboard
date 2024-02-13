Red [
	description: {"Difference of Squares" exercise solution for exercism platform}
	author: "sabnocke"
]

square-of-sum: function [
	number[integer!]
    return: [integer!]
] [
    sum: 0
    until [
        sum: sum + number
        number: number - 1
        number = 0
    ]
    return sum ** 2
]

sum-of-squares: function [
	number[integer!]
    return: [integer!]
] [
	sum: 0
    until [
        sum: sum + (number ** 2)
        number: number - 1
        number = 0
    ]
    return sum
]

difference-of-squares: function [
	number[integer!]
    return: [integer!]
] [
	return subtract square-of-sum number sum-of-squares number
]

print sum-of-squares 10
print square-of-sum 10
print difference-of-squares 10