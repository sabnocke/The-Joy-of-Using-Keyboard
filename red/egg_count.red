Red [
	description: {"Eliud's Eggs" exercise solution for exercism platform}
	author: "sabnocke"
]

egg-count: function [
	number[integer!]
    return: [integer!]
] [
	return sum binary number
]

binary: function [
    number[integer!]
    return: [block!]
] [
    bin: []
    while [not zero? number] [
        either zero? number and 1 [append bin 0][append bin 1]
        number: number >> 1
    ]
    return reverse bin

]

