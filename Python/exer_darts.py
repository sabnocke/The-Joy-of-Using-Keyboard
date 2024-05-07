import numpy as np


def score(x: float, y: float) -> int:
    vec = np.array([x, y])
    length = np.linalg.norm(vec)
    if length > 10:
        return 0
    if 10 >= length > 5:
        return 1
    if 5 >= length > 1:
        return 5
    return 10


if __name__ == "__main__":
    s: str = "the quick brown fox jumps over the lazy dog"
    reduced_sentence = filter(lambda x: 123 >= ord(x) >= 97, set(s))
    print(list(reduced_sentence))
