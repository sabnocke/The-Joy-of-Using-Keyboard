Red [
	description: {"Grains" exercise solution for exercism platform}
	author: "sabnocke"
]

square: function [
	square
] [
    if (square < 1) or (square > 64) [cause-error 'user 'message ["square must be between 1 and 64"]]
    return 2 ** (square - 1)
]

total: function [] [
	return (1 - (2 ** 64))/(-1)
]

print square 4
print total