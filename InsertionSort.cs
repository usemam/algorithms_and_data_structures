using System;

namespace ADS
{
    public class InsertionSort
    {
        public static void Main()
        {
            var aStr = Console.ReadLine().Split(' ');
            var n = aStr.Length;
            var a = new int[n];
            for (int i = 0; i < n; ++i) a[i] = Convert.ToInt32(aStr[i]);

            Sort(a);
            for (int i = 0; i < n; ++i) Console.Write("{0} ", a[i]);
            Console.ReadLine();
        }

        private static void Sort(int[] a)
        {
            int n = a.Length;
            for (int i = 1; i < n; ++i)
            {
                int tmp = a[i], j;
                for (j = i - 1; j >= 0 && tmp < a[j]; --j)
                    a[j + 1] = a[j];
                a[j + 1] = tmp;
            }
        }
    }
}