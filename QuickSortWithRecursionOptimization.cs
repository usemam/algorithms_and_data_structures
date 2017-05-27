/*
 * Дан массив целых чисел в диапазоне [0..10^9]. Размер массива кратен 10 и ограничен сверху значением 2 * 10^6 элементов.
 * Все значения массива являются элементами псевдо-рандомной последовательности.
 * Необходимо отсортировать элементы массива за минимально время с использованием быстрой сортировки
 * и вывести каждый десятый элемент отсортированной последовательности через пробел.
 * Минимальный набор оптимизаций, который необходимо реализовать:
 * 1. Оптимизация выбора опорного элемента.
 * 2. Оптимизация концевой рекурсии.
 * 
 * Рекурсия отнимает много времени, т.к. каждый вызов функции - достаточно трудоемкая операция.
 * Особенно часто рекурсивная функция сортировки QuickSort вызывается на маленьких подмассивах.
 * Под оптимизацией концевой рекурсии предполагается не доводить рекурсию до конца - до размера 1.
 * А остановиться, если оставшийся размер исследуемого подмассива меньше некоторого порога. Например, 40.
 * Затем можно отсортировать оставшиеся куски нерекурсивной сортировкой. Например, InsertionSort.
 * Да, она квадратичная, но, как ни странно, будет работать быстрее на маленьких массивах.
 * Более того, InsertionSort можно вызывать не для каждого оставшегося куска отдельно,
 * а вызвать InsertionSort один раз для всего массива.
 * Просьба не использовать стандартные функции сортировки. Пишите свою.
 */

using System;

namespace ADS
{
    public class QuickSortWithRecursionOptimization
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