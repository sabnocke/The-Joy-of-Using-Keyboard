from concurrent.futures import ProcessPoolExecutor, ThreadPoolExecutor, as_completed
from urllib.request import urlopen
import math
import time


URLS = ['http://www.foxnews.com/',
        'http://www.cnn.com/',
        'http://europe.wsj.com/',
        'http://www.bbc.co.uk/',
        'http://nonexistant-subdomain.python.org/']

def runtime(func):
    def wrapper(*args, **kwargs):
        start = time.time()
        result = func(*args, **kwargs)
        end = time.time()
        print(f'{func.__name__} ran in {end - start} seconds')
        return result
    return wrapper

def load_url(url, timeout=None):
    # Retrieve a single page and report the URL and contents
    with urlopen(url, timeout=timeout) as response:
        data = response.read()
        return data

@runtime    
def threading_test():
    # We can use a with statement to ensure threads are cleaned up promptly
    with ThreadPoolExecutor(max_workers=2) as executor:
        future_to_url = {executor.submit(load_url, url, 60): url for url in URLS}
        for future in as_completed(future_to_url):
            url = future_to_url[future]
            try:
                data = future.result()
            except Exception as exc:
                print('%r generated an exception: %s' % (url, exc))
            else:
                print('%r page is %d bytes' % (url, len(data)))

PRIMES = [
    112272535095293,
    112582705942171,
    112272535095293,
    115280095190773,
    115797848077099,
    1099726899285419]

def is_prime(num: int) -> bool:
    if num < 2:
        return False
    if num == 2:
        return True
    if num % 2 == 0:
        return False
    
    sqrt_n: int = int(math.floor(math.sqrt(num))) + 1
    for i in range(3, sqrt_n, 2):
        if num % i == 0:
            return False
    return True



@runtime
def processing_test():
    with ProcessPoolExecutor() as pool:
        for number, prime in zip(PRIMES, pool.map(is_prime, PRIMES)):
            print(f'{number} is {prime}')
            
@runtime
def loop_test():
    for item in PRIMES:
        print(f"{item} is {is_prime(item)}")

def main():
    processing_test()
            
if __name__ == "__main__":
    main()