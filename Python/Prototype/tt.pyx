from libc.stdlib cimport malloc, free

def get_prime(int amount):
    cdef int N = amount + 1
    cdef int *primes = <int *> malloc(N * sizeof(int))
    prime_values = []
    for i in range(N):
        primes[i] = True
    cdef int p = 2
    while p ** 2 <= amount:
        if primes[p] != b'\x00':
            for i in range(p ** 2, amount + 1, p):
                primes[i] = 0
        p += 1

    for n in range(2, N):
        if primes[n] != b'\x00':
            prime_values.append(n)
    # release memory once done
    free(<void *> primes)
    return prime_values
