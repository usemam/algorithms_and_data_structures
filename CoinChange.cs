/*
 * This one is not from a course, but rather is well-known algorithmic challenge.
 * You have an array X[0..n], which represents a finite supply of coins of different values.
 * You can get a change K from supply X, if you can select such elements from X whose total sum of values equals to K.
 * Build an algorithm that will return 'true' if you can get a change K from X, and will return 'false' otherwise.
 */

using System;

namespace ADS
{
    public class CoinChange
    {
        public static void Main()
        {
            int k = Convert.ToInt32(Console.ReadLine());
            var str = Console.ReadLine().Split();
            int n = str.Length;
            var x = new int[n];
            for (int i = 0; i < n; ++i) x[i] = Convert.ToInt32(str[i]);
            Console.WriteLine(Naive(x, n-1, k));
            Console.ReadLine();
        }

        private static bool Naive(int[] x, int n, int k)
        {
            if (k == 0) return true;
            if (k < 0 || n < 0) return false;
            return Naive(x, n - 1, k) || Naive(x, n - 1, k - x[n]);
        }
    }
}