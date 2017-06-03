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
            Console.WriteLine(Array(x, n, k));
            Console.ReadLine();
        }

        /// <summary>
        /// Naive recursive implementation. Running time is O(2^n).
        /// </summary>
        private static bool Naive(int[] x, int n, int k)
        {
            if (k == 0) return true;
            if (k < 0 || n < 0) return false;
            return Naive(x, n - 1, k) || Naive(x, n - 1, k - x[n]);
        }

        /// <summary>
        /// Optimized solution that uses boolean matrix. Running time is O(n*k), memory usage is O(n*k).
        /// </summary>
        private static bool Matrix(int[] x, int n, int k)
        {
            var m = new bool[n + 1, k + 1];
            m[0, 0] = true;
            for (int i = 1; i <= n; i++)
            {
                m[i, 0] = true;
                for (int j = 1; j <= k; ++j)
                    m[i, j] = m[i - 1, j] || x[i - 1] <= j && m[i - 1, j - x[i - 1]];
            }
            return m[n, k];
        }

        /// <summary>
        /// Optimized solution that uses two boolean arrays. Running time is O(n*k), memory usage is O(k).
        /// </summary>
        private static bool Array(int[] x, int n, int k)
        {
            var a1 = new bool[k + 1];
            a1[0] = true;
            var a2 = new bool[k + 1];
            for (int i = 0; i < n; i++)
            {
                a2[0] = true;
                for (int j = 1; j <= k; j++)
                    a2[j] = a1[j] || x[i] <= j && a1[j - x[i]];
                for (int j = 1; j <= k; j++) a1[j] = a2[j];
            }

            return a1[k];
        }
    }
}