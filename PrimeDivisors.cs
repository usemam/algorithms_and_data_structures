/*
 * Выведите разложение натурального числа n > 1 на простые множители.
 * Простые множители должны быть упорядочены по возрастанию и разделены пробелами.
*/

using System;
using System.Collections.Generic;

namespace ADS
{
    public class PrimeDivisors
    {
        private static bool IsPrime(int n, HashSet<int> primes)
        {
            for (int i = 2; i * i <= n; ++i)
            {
                if (n % i == 0) return false;
            }

            if (!primes.Contains(n)) primes.Add(n);
            return true;
        }

        public static void Main()
        {
            // read int
            int n = Convert.ToInt32(Console.ReadLine());
            var divisors = new List<int>();
            var primes = new HashSet<int>();
            while (n > 1)
            {
                for (int i = 2; i <= n; i++)
                {
                    if (!IsPrime(i, primes)) continue;
                    if (n % i == 0)
                    {
                        divisors.Add(i);
                        n /= i;
                        break;
                    }
                }
            }

            divisors.Sort();
            foreach (int divisor in divisors)
            {
                Console.Write("{0} ",divisor);
            }

            Console.ReadLine();
        }
    }
}
