using System;

namespace ADS
{
    public class Arrays1
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