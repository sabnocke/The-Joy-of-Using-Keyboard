Red [
    description: {"Resistor Color" exercise solution for exercism platform}
	author: "sabnocke"
]

res: ["black" "brown" "red" "orange" "yellow" "green" "blue" "violet" "grey" "white"]

color-code: function [
	color[string!]
] [
    length? difference res (find res color)
]

colors: function [] [
	return res
]

print color-code "green"
print colors