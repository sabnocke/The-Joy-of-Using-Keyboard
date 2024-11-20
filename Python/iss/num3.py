import numpy as np
from icecream import ic

N = 8
n = np.arange(N)

A = np.zeros([N//2 + 1, N], dtype=np.complex64)

A[1, :] = np.exp(-1j * (np.pi/4 + 2 * np.pi * 1/N * n))
for row in A:
    print(f"{row[0]:.2f} {row[1]:.2f} {row[2]:.2f} {row[3]:.2f} {row[4]:.2f} {row[5]:.2f} {row[6]:.2f} {row[7]:.2f}")
