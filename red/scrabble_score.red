Red [
	description: {"Scrabble Score" exercise solution for exercism platform}
	author: "sabnocke"
]


contain?: func [
    src[series!]
    fnd
    return:[logic!]
] [
    return not none? find src (to-string fnd)
]

score: function [
	word
] [
    points: 0
    word: uppercase word
    foreach char word [
        case [
            contain? ["A" "E" "I" "O" "U" "L" "N" "R" "S" "T"] char [points: points + 1]
            contain? ["D" "G"] char [points: points + 2]
            contain? ["B" "C" "M" "P"] char [points: points + 3]
            contain? ["F" "H" "V" "W" "Y"] char [points: points + 4]
            contain? ["K"] char [points: points + 5]
            contain? ["J" "X"] char [points: points + 8]
            contain? ["Q" "Z"] char [points: points + 10]
        ]
    ]
    return points

]

test: func [
    value[string!]
    expected[integer!]
] [
    val: score value
    either (score value) = expected [print true][print [value "!=" expected]]

]
score "cabbage"
score "a"
score "zoo"
score "street"
score "quirky"

test "cabbage" 14
test "a" 1
test "zoo" 12
test "street" 6
test "quirky" 22
test "OxyphenButazone" 41