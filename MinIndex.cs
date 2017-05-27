/*
 * Дан отсортированный массив различных целых чисел A[0..n-1] и массив целых чисел B[0..m-1].
 * Для каждого элемента массива B[i] найдите минимальный индекс элемента массива A[k],
 * ближайшего по значению к B[i].
 * Время работы поиска для каждого элемента B[i]: O(log(k)).
 * Подсказка. Обратите внимание, что время работы должно зависеть от индекса ответа - k.
 * Для достижения такой асимптотики предлагается для начала найти отрезок вида [2p,2p+1],
 * содержащий искомую точку, а уже затем на нём выполнять традиционный бин. поиск.
 */
using System;

namespace ADS
{
    public class MinIndex
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