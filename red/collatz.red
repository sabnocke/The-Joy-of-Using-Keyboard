Red [
	description: {"Collatz Conjecture" exercise solution for exercism platform}
	author: "Me"
]

steps: function [
	number[integer!]
] [
    if number < 1 [ cause-error 'user 'message "can only use positive integers" ]
    if number = 1 [ return 0 ]
    steps: 0
    while [number <> 1] [
        steps: steps + 1
        case [
            even? number [number: number / 2]
            odd? number [number: 3 * number + 1]
        ]

    ]
    return steps
]