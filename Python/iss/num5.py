from math import sqrt
import numpy as np
from icecream import ic

g = 1/sqrt(2)
conjugate = lambda x: np.conj(x)
N = 8

Xs = np.array([
    4,
    1 + 0.5 * g + 1j * (0.5 * g + 0.5),
    -0.5 + 1.5j,
    1 - 0.5 * g + 1j * (0.5 * g + 0.5),
    1,
    1 - 0.5 * g - 1j * (0.5 * g + 0.5),
    -0.5 - 1.5j,
    1 + 0.5 * g - 1j * (0.5 * g + 0.5),
], dtype=np.complex64)

X = np.array([
    4,
    1 + 0.5 * g + 1j * (0.5 * g + 0.5),
    -0.5 + 1.5j,
    1 - 0.5 * g + 1j * (0.5 * g + 0.5),
    1
], dtype=np.complex64)

o = np.array([np.conj(X[N - i]) for i in range(N//2 + 1, N)], dtype=complex)
res = np.concatenate((X, o))
ic(res, Xs, res == Xs)
