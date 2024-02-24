Red [
	description: {"Space Age" exercise solution for exercism platform}
	author: "sabnocke"
    ]


age: function [
	planet[string!]
	seconds[integer!]
] [
    planet: lowercase planet
    earth: 31557600
    if planet = "earth" [
        return round/to seconds / planet 0.01
    ]
	return round/to (seconds / earth) / (select planets planet) 0.01
]

planets: [
    "mercury" 0.2408467
    "venus" 0.61519726
    "mars" 1.8808158
    "jupiter" 11.862615
    "saturn" 29.447498
    "uranus" 84.016846
    "neptune" 164.79132
]

print age "Mercury" 2134835688