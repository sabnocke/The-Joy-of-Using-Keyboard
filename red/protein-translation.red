Red [
	description: {"Protein Translation" exercise solution for exercism platform}
	author: "sabnocke"
]


co-pro: [
    "AUG" "Methionine"
    "UUU" "Phenylalanine"
    "UUC" "Phenylalanine"
    "UUA" "Leucine"
    "UUG" "Leucine"
    "UCU" "Serine"
    "UCC" "Serine"
    "UCA" "Serine"
    "UCG" "Serine"
    "UAU" "Tyrosine"
    "UAC" "Tyrosine"
    "UGU" "Cysteine"
    "UGC" "Cysteine"
    "UGG" "Trytophane"
    "UAA" "STOP"
    "UAG" "STOP"
    "UGA" "STOP"
]

proteins: function [
	strand
] [
    proteins: []
    while [not empty? strand] [
        token: take/part strand 3
        protein: select co-pro token
        either protein = "STOP" [break][append proteins protein]
    ]
    return proteins
]
