using System;

namespace ADS
{
    public class Sort6
    {
        private static int[] _five = new int[5];

        public static void Main()
        {
            var nk = Console.ReadLine().Split(' ');
            int n = Convert.ToInt32(nk[0]);
            int k = Convert.ToInt32(nk[1]);

            var aStr = Console.ReadLine().Split(' ');
            int[] a = new int[n];
            for (int i = 0; i < n; ++i) a[i] = Convert.ToInt32(aStr[i]);

            int kth = Select(a, 0, n - 1, k);
            Console.WriteLine(a[kth]);
            Console.ReadLine();
        }

        private static int Select(int[] a, int left, int right, int k)
        {
            while (left != right)
            {
                int pi = Pivot(a, left, right);
                pi = Partition(a, left, right, pi);
                if (k == pi) return pi;
                if (k < pi) right = pi - 1;
                else left = pi + 1;
            }

            return left;
        }

        private static int Partition(int[] a, int left, int right, int pi)
        {
            int piValue = a[pi];
            Swap(a, pi, right);
            int j = left;
            for (int i = left; i < right; ++i)
                if (a[i] < piValue) Swap(a, j++, i);
            Swap(a, right, j);

            return j;
        }

        private static int Pivot(int[] a, int left, int right)
        {
            if (right - left < 5) return Med5(a, left, right);

            for (int i = left; i <= right; i += 5)
            {
                int r = Math.Min(i + 4, right);
                int med = Med5(a, i, r);
                Swap(a, med, left + (int)Math.Floor((i - left) / 5.0));
            }

            return Select(
                a, left, left + (int)Math.Ceiling((right - left) / 5.0) - 1, left + (right - left) / 10);
        }

        private static int Med5(int[] a, int left, int right)
        {
            if (right - left <= 1) return left;

            int n = right - left + 1;
            for (int i = 0; i < n; ++i) _five[i] = a[left + i];
            for (int i = 1; i < n; ++i)
            {
                int tmp = _five[i], j;
                for (j = i - 1; j >= 0 && tmp < _five[j]; --j)
                    _five[j + 1] = _five[j];
                _five[j + 1] = tmp;
            }

            int med = _five[n / 2];
            for (int i = 0; i < n; ++i)
                if (a[left + i] == med) return left + i;
            return left;
        }

        private static void Swap(int[] a, int i, int j)
        {
            int tmp = a[i];
            a[i] = a[j];
            a[j] = tmp;
        }
    }
}