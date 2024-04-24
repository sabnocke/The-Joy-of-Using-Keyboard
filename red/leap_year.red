Red [
	description: {"Leap" exercise solution for exercism platform}
	author: "Me, partially" ; you can write your name here, in quotes
]



leap: function [
	year[integer!]
] [
    case [
        all[ zero? year // 100 zero? year // 400 ][ return true]
        all[ year // 100 <> 0 zero? year // 4] [ return true ]
        true [return false]
    ]
]

leap2: func [
    year[integer!]
] [
    sum: 0
    print year
    foreach n [4 100 400][ sum: sum + sign? year // n ]
    even? sum
]