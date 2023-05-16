namespace testGround;

public static class Erast
{
    public static int[] Sieve(int amount)
    {
        bool[] primes = Enumerable.Repeat(true, amount + 1).ToArray();
        int p = 2;
        while (p * p <= amount)
        {
            if (primes[p])
            {
                var p1 = p;
                var itr = Enumerable
                    .Range(p*p, amount + 1 - p*p)
                    .Where(x => (x - p1*p1) % p1 == 0);
                foreach (var n in itr)
                {
                    primes[n] = false;
                }
            }
            p += 1;
        }
        return Enumerable.Range(2, amount - 1).Where(x => primes[x]).ToArray();
    }
}