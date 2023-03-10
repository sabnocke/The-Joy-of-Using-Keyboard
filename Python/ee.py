from Prototype.tt import get_prime
import time


def timer(func):
    def wrapper(*args, **kwargs):
        start = time.time()
        result = func(*args, **kwargs)
        end = time.time()
        print("Execution time: {:.6f} seconds".format(end - start))
        return result
    return wrapper


@timer
def prime_gen(amount: int):
    n = amount + 1
    primes = [True for _ in range(n)]
    p = 2
    while p ** 2 <= amount:
        if primes[p]:
            for i in range(p ** 2, n, p):
                primes[i] = False
        p += 1

    return [i for i in range(2, n) if primes[i]]


@timer
def insert_int(_int: int):
    return get_prime(_int)


funcs = [insert_int, prime_gen]
prime_gen(20000)
insert_int(20000)
