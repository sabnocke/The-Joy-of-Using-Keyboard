import numpy as np
from icecream import ic


N = 8
x = np.array([1, 1, 1, 0, 0, 0.5, 0.5, 0])
n = np.arange(N)
k = np.arange(N//2 + 1)

b_c = np.zeros([N//2 + 1, N])
a_c = np.zeros([N//2 + 1, N])

a_c[k, :] = np.cos(2 * np.pi * k[:, np.newaxis] / N * n)
b_c[k, :] = np.sin(2 * np.pi * k[:, np.newaxis] / N * n)


c_a = np.sum(a_c * x.T, axis=1)
c_b = np.sum(b_c * x.T, axis=1)
ic(c_a, c_b)

X = np.complex64(c_a + 1j * c_b)
# ic(np.abs(X), np.angle(X))
ic(X)
d_c = np.zeros([N//2 + 1, N], dtype=np.complex64)

d_c[k, :] = np.exp(1j * 2 * np.pi * k[:, np.newaxis] / N * n)
c_d = np.sum(d_c * x.T, axis=1, dtype=np.complex64)
ic(c_d)

con_d_c = np.zeros([N//2 + 1, N], dtype=np.complex64)
con_d_c[k, :] = np.exp(-1j * 2 * np.pi * k[:, np.newaxis] / N * n)

c_con_d_c = np.sum(con_d_c * x.T, axis=1, dtype=np.complex64)
ic(c_con_d_c)