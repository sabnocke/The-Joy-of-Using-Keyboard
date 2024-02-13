Red [
	description: {"Hamming" exercise solution for exercism platform}
	author: "sabnocke"
]

distance: function [
	strand1
	strand2
] [
	if (length? strand1) - (length? strand2) <> 0 [return -1]
    mistakes: 0
    repeat count length? strand1 [
        if strand1/(count) <> strand2/(count) [mistakes: mistakes + 1]
    ]
    return mistakes
]

print distance "Hello" "Hulla"