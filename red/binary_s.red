Red []



binary_s: func [array[block!] value[integer!] .min[integer!] .max[integer!]] [
    if not find array value [return "value not in array"]
    if .min > .max [ return -1 ]
    mid: to integer! round/ceiling (.min + .max) / 2
    bmid: 1 + (.min + .max) >> 1
    case [
        value == array/(mid) [return mid]
        value < array/(mid) [return binary_s array value .min mid]
        value > array/(mid) [return binary_s array value (mid + 1) .max]
    ]
]

basic: [1 2 3 4 5 6 7 8 9 10]

size: length? basic
print binary_s basic 9 0 size
