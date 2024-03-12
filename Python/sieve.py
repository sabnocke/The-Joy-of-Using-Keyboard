import math


def sieve(n: int):
    primes = [True] * (n + 1)
    primes[0] = primes[1] = False
    for i in range(4, n + 1, 2):
        primes[i] = False
    for i in range(3, n + 1):
        if primes[i]:
            for j in range(i ** 2, n + 1, i << 1):
                primes[j] = False
    prime_numbers = [prime for prime in range(2, n + 1) if primes[prime]]
    return prime_numbers


def is_prime(n: int) -> bool:
    for i in range(2, int(math.sqrt(n)) + 1):
        print(i)
        if n % i == 0:
            return False
    return True


def n_primes(n: int) -> list:
    p = 2
    primes = []
    while n > 0:
        if is_prime(p):
            primes.append(p)
            n -= 1
        p += 1
    return primes


if __name__ == "__main__":
    print(is_prime(2))
    print(sieve(10))
