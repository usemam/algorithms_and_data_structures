using System;

namespace ADS
{
    public class Arrays2
    {
        public static void Main()
        {
            int n = Convert.ToInt32(Console.ReadLine());

            var a = new int[n];
            var aStr = Console.ReadLine().Split(' ');
            for (int i = 0; i < n; ++i)
                a[i] = Convert.ToInt32(aStr[i]);

            int m = Convert.ToInt32(Console.ReadLine());
            var b = new int[m];
            var bStr = Console.ReadLine().Split(' ');
            for (int i = 0; i < m; ++i)
                b[i] = Convert.ToInt32(bStr[i]);

            foreach (int e in b)
            {
                int i = BinarySearch(a, e);
                Console.Write("{0} ", i);
            }

            Console.ReadLine();
        }

        private static int BinarySearch(int[] a, int e)
        {
            int first = 0;
            int count = a.Length;
            int last = count;
            while (first < last)
            {
                int mid = (first + last) / 2;
                if (mid == (count - 1) || Math.Abs(e - a[mid + 1]) >= Math.Abs(e - a[mid])) last = mid;
                else first = mid + 1;
            }

            if (first == count) return first - 1;
            return first;
        }
    }
}