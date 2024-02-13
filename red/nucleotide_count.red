Red [
	description: {"Nucleotide Count" exercise solution for exercism platform}
	author: "sabnocke"
]


correct?: func [
    strand[string!]
    return: [logic!]
] [
    return (union "ACGT" strand) = "ACGT"
]

nucleotide-counts: function [
	strand
] [
    unless ((correct? strand) or (empty? strand)) [cause-error 'user 'message "Invalid nucleotide in strand"]
    nucleotides:  #[A: 0 C: 0 G: 0 T: 0]
    foreach n strand [
        token: select nucleotides to-word n
        if number? token [put nucleotides to-word n token + 1]
    ]
    return nucleotides
]

print nucleotide-counts ""
print nucleotide-counts "A"
