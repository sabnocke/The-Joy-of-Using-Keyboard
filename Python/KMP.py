def kmp_search(text, pattern):
    text_length, pattern_length = len(text), len(pattern)
    next = compute_prefix_function(pattern)
    index_t, index_p = 0, 0
    while index_t < text_length:
        if text[index_t] == pattern[index_p]:
            index_t += 1
            index_p += 1
        if index_p == pattern_length:
            print("Podřetězec nalezen na pozici", index_t - index_p)
            index_p = next[index_p-1]
        elif index_t < text_length and text[index_t] != pattern[index_p]:
            if index_p != 0:
                index_p = next[index_p-1]
            else:
                index_t += 1

def compute_prefix_function(pattern):
    # vraci list prefixu, napr. "kokokosovy":"kokos" = [0, 0, 1, 2, 0]
    length = len(pattern)
    next = [0] * length
    rnd_i, rnd_j = 1, 0
    while rnd_i < length:
        if pattern[rnd_i] == pattern[rnd_j]:
            rnd_j += 1
            next[rnd_i] = rnd_j
            rnd_i += 1
        elif rnd_j == 0:
            next[rnd_i] = 0
            rnd_i += 1
        else:
            rnd_j = next[rnd_j-1]
    print(next)
    return next

text = "kokokosovy"
kmp_search(text, "kokos")
compute_prefix_function("abrakadabra")
