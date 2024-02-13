Red [
	description: {"Raindrops" exercise solution for exercism platform}
	author: "sabnocke"
]

convert: function [
	number
] [
    answer: rejoin collect [foreach [k v] [3 "Pling" 5 "Plang" 7 "Plong"] [
        either zero? number // k [keep v][continue]
        ]]
    return either empty? answer [to string! number][answer]
]


