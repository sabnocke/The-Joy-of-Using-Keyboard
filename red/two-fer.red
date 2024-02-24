Red [
	description: {"Two-fer" exercise solution for exercism platform}
	author: "sabnocke"
]

two-fer: function [
	name [string! none!]
    return:[string!]
] [
	return either none? name ["One for you, one for me."] [rejoin ["One for " name ", one for me."]]
]

print two-fer none
print two-fer "Alice"