using System;

namespace ADS
{
    public class Sort5
    {
        private const int SizeThreshold = 5;

        public static void Main()
        {
            var aStr = Console.ReadLine().Split(' ');
            int n = aStr.Length;
            int[] a = new int[n];
            for (int i = 0; i < n; ++i) a[i] = Convert.ToInt32(aStr[i]);

            QuickSort(a, 0, n-1);
            InsertionSort(a);

            for (int i = 9; i < n; i += 10)
                Console.Write("{0} ", a[i]);
            Console.ReadLine();
        }

        private static void InsertionSort(int[] a)
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

        private static void QuickSort(int[] a, int lo, int hi)
        {
            if (hi-lo <= SizeThreshold) return;

            while (hi - lo > SizeThreshold)
            {
                int pi = Partition(a, lo, hi);
                if (pi - lo < hi - pi)
                {
                    QuickSort(a, lo, pi-1);
                    lo = pi + 1;
                }
                else
                {
                    QuickSort(a, pi + 1, hi);
                    hi = pi - 1;
                }
            }
        }

        private static int Partition(int[] a, int lo, int hi)
        {
            if (hi -lo <= 1) return lo;

            int pivot = a[hi];
            int i = lo, j = hi - 1;
            while (i <= j)
            {
                for (;a[i]<pivot;++i) { }
                for (;j>=lo && a[j] >= pivot;--j) { }
                if (i < j) Swap(a, i++, j++);
            }

            Swap(a, i, hi);
            return i;
        }

        private static void Swap(int[] a, int i, int j)
        {
            var tmp = a[i];
            a[i] = a[j];
            a[j] = tmp;
        }
    }
}