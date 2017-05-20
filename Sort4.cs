using System;

namespace ADS
{
    public class Sort4
    {
        public static void Main()
        {
            int n = Convert.ToInt32(Console.ReadLine());
            var strings = Console.ReadLine().Split(' ');
            var a = new int[n];
            for (int i = 0; i < n; ++i)
                a[i] = Convert.ToInt32(strings[i]);
            int k = Convert.ToInt32(Console.ReadLine());

            for (int i = 0; i < n; i = i + k) MergeSort(a, i, Math.Min(i + k, n - 1));

            for (int i = 0; i < n; ++i) Console.Write("{0} ", a[i]);
            Console.ReadLine();
        }

        private static void MergeSort(int[] a, int lo, int hi)
        {
            if (hi - lo <= 0) return;

            int mid = (hi + lo) / 2;
            MergeSort(a, lo, mid);
            MergeSort(a, mid + 1, hi);
            Merge(a, lo, hi);
        }

        private static void Merge(int[] a, int lo, int hi)
        {
            var sorted = new int[hi - lo + 1];
            int mid = (hi + lo) / 2;
            int i = lo, j = mid + 1;
            while (i <= mid && j <= hi)
            {
                int ix = (i - lo) + (j - (mid + 1));
                if (a[i] <= a[j])
                {
                    sorted[ix] = a[i];
                    i++;
                }
                else
                {
                    sorted[ix] = a[j];
                    j++;
                }
            }

            if (i > mid)
                for (; j <= hi; ++j)
                {
                    int ix = (i - lo) + (j - (mid + 1));
                    sorted[ix] = a[j];
                }
            else
                for (; i <= mid; ++i)
                {
                    int ix = (i - lo) + (j - (mid + 1));
                    sorted[ix] = a[i];
                }
            for (int k = lo; k <= hi; ++k)
                a[k] = sorted[k - lo];
        }
    }
}