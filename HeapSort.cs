/*
 * Задано N точек на плоскости. Указать (N-1)-звенную несамопересекающуюся незамкнутую ломаную, проходящую через все эти точки.
 * Стройте ломаную в порядке возрастания x-координаты. Если имеются две точки с одинаковой x-координатой,
 * то расположите раньше ту точку, у которой y-координата меньше.
 * Для сортировки точек реализуйте пирамидальную сортировку.
 */

using System;

namespace ADS
{
    public class HeapSort
    {
        private static Point[] _points;

        private static int _size;

        public static void Main()
        {
            int n = Convert.ToInt32(Console.ReadLine());

            _points = new Point[n];
            for (int i = 0; i < n; i++)
            {
                var input = Console.ReadLine().Split(' ');
                _points[i] =
                    new Point
                    {
                        X = Convert.ToInt32(input[0]),
                        Y = Convert.ToInt32(input[1])
                    };
            }

            Sort();
            foreach (var p in _points)
                Console.WriteLine("{0} {1}", p.X, p.Y);
            Console.ReadLine();
        }

        private static void Sort()
        {
            // build heap
            _size = _points.Length;
            for (int i = _size / 2 - 1; i >= 0; --i) SiftDown(i);
            // sort
            while (_size > 1)
            {
                Swap(0, _size - 1);
                _size--;
                SiftDown(0);
            }
        }

        private static void Swap(int i, int j)
        {
            Point tmp = _points[i];
            _points[i] = _points[j];
            _points[j] = tmp;
        }

        private static void SiftDown(int i)
        {
            int left = i * 2 + 1;
            int right = i * 2 + 2;
            int hi = i;
            if (left < _size && _points[left].MoreThan(_points[i])) hi = left;
            if (right < _size && _points[right].MoreThan(_points[hi])) hi = right;
            if (hi != i)
            {
                Swap(i, hi);
                SiftDown(hi);
            }
        }

        private struct Point
        {
            public int X { get; set; }
            public int Y { get; set; }

            public bool MoreThan(Point other)
            {
                return
                    X > other.X ||
                    X == other.X && Y > other.Y;
            }
        }
    }
}