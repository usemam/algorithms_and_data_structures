/*
 * Даны два массива целых чисел одинаковой длины A[0..n−1]A[0..n−1] и B[0..n−1]B[0..n−1].
 * Необходимо найти первую пару индексов i0 и j0, i0≤j0, такую что A[i0]+B[j0]=max{A[i]+B[j],где0≤i<n,0≤j<n,i≤j}.
 * Время работы – O(n).
 * Ограничения: 1≤n≤100000,0≤A[i]≤100000,0≤B[i]≤100000  для любого ii.
 */

using System;

namespace ADS
{
    public class MaxSum
    {
        public static void Main()
        {
            int n = Convert.ToInt32(Console.ReadLine());

            var a = new int[n];
            var aStr = Console.ReadLine().Split(' ');
            for (int i = 0; i < n; ++i)
                a[i] = Convert.ToInt32(aStr[i]);

            var b = new int[n];
            var bStr = Console.ReadLine().Split(' ');
            for (int i = 0; i < n; ++i)
                b[i] = Convert.ToInt32(bStr[i]);

            int i0 = n, j0 = n, iMaxB = n, maxB = int.MinValue, maxT = int.MinValue;
            for (int i = n - 1; i >= 0; --i)
            {
                if (b[i] > maxB)
                {
                    maxB = b[i];
                    iMaxB = i;
                }

                if (a[i] + maxB >= maxT)
                {
                    maxT = a[i] + maxB;
                    i0 = i;
                    j0 = iMaxB;
                }
            }

            Console.WriteLine("{0} {1}", i0, j0);
            Console.ReadLine();
        }
    }
}