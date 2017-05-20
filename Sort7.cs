using System;

namespace ADS
{
    public class Sort7
    {
        public static void Main()
        {
            int n = Convert.ToInt32(Console.ReadLine());
            var a = new long[n];

            ReadArray(ref a);

            LSDSort(a, n);
            for (int i = 0; i < n; ++i) Console.Write("{0} ", a[i]);
            Console.ReadLine();
        }

        private static void ReadArray(ref long[] a)
        {
            string sData = Console.ReadLine();
            int i = 0;
            long val = 0;
            long dec = 1;
            for (int j = sData.Length - 1; j >= 0; j--)
            {
                char c = sData[j];

                if (c == ' ')
                {
                    a[i++] = val;
                    val = 0;
                    dec = 1;
                }
                else if (c >= '0' && c <= '9')
                {
                    val += (c - '0') * dec;
                    dec *= 10;
                }
            }
            a[a.Length - 1] = val;
        }

        private static void LSDSort(long[] a, int n)
        {
            for (int r = 0; r < 8; ++r) CountingSort(a, n, r);
        }

        private static void CountingSort(long[] a, int n, int r)
        {
            int k = 256;
            var c = new int[k];
            foreach (long v in a) ++c[GetByte(v, r)];

            int sum = 0;
            for (int i = 0; i < k; ++i)
            {
                int tmp = c[i];
                c[i] = sum;
                sum += tmp;
            }

            var b = new long[n];
            for (int i = 0; i < n; ++i) b[c[GetByte(a[i], r)]++] = a[i];
            for (int i = 0; i < n; ++i) a[i] = b[i];
        }

        private static int GetByte(long v, int r)
        {
            return (int)((v >> (8 * r)) & 0xff);
        }
    }
}