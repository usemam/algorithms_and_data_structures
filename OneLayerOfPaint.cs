/*
 * На числовой прямой окрасили N отрезков. Известны координаты левого и правого концов каждого отрезка (Li и Ri).
 * Найти сумму длин частей числовой прямой, окрашенных ровно в один слой.
 * Для сортировки реализуйте сортировку слиянием.
 */

using System;

namespace ADS
{
    public class OneLayerOfPaint
    {
        public static void Main()
        {
            int n = Convert.ToInt32(Console.ReadLine());
            int len = n * 2;
            var points = new Point[len];
            for (int i = 0; i < len; i = i+2)
            {
                var line = Console.ReadLine().Split(' ');
                points[i] =
                    new Point
                    {
                        Value = Convert.ToInt32(line[0]),
                        IsFinish = false
                    };
                points[i + 1] =
                    new Point
                    {
                        Value = Convert.ToInt32(line[1]),
                        IsFinish = true
                    };
            }

            MergeSort(points, 0, len-1);

            int layers = 0, left = 0, sum = 0;
            foreach (Point point in points)
            {
                if (layers == 1) sum += point.Value - left;
                if (point.IsFinish) layers--;
                else layers++;
                left = point.Value;
            }

            Console.WriteLine(sum);
            Console.ReadLine();
        }

        private static void MergeSort(Point[] points, int lo, int hi)
        {
            if (hi - lo <= 0) return;

            int mid = (hi + lo) / 2;
            MergeSort(points, lo, mid);
            MergeSort(points, mid + 1, hi);
            Merge(points, lo, hi);
        }

        private static void Merge(Point[] points, int lo, int hi)
        {
            var sorted = new Point[hi - lo + 1];
            int mid = (hi + lo) / 2;
            int i = lo, j = mid + 1;
            while (i <= mid && j <= hi)
            {
                int ix = (i - lo) + (j - (mid + 1));
                if (points[i].Value <= points[j].Value)
                {
                    sorted[ix] = points[i];
                    i++;
                }
                else
                {
                    sorted[ix] = points[j];
                    j++;
                }
            }

            if (i > mid)
                for (; j <= hi; ++j)
                {
                    int ix = (i - lo) + (j - (mid + 1));
                    sorted[ix] = points[j];
                }
            else
                for (; i <= mid; ++i)
                {
                    int ix = (i - lo) + (j - (mid + 1));
                    sorted[ix] = points[i];
                }
            for (int k = lo; k <= hi; ++k)
                points[k] = sorted[k - lo];
        }

        public struct Point
        {
            public int Value { get; set; }
            public bool IsFinish { get; set; }
        }
    }
}