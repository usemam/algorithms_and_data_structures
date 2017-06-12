/*
 * Жадина.
 * Вовочка ест фрукты из бабушкиной корзины. В корзине лежат фрукты разной массы.
 * Вовочка может поднять не более K грамм. Каждый фрукт весит не более K грамм.
 * За раз он выбирает несколько самых тяжелых фруктов, которые может поднять одновременно,
 * откусывает от каждого половину и кладет огрызки обратно в корзину. Если фрукт весит нечетное число грамм,
 * он откусывает большую половину. Фрукт массы 1гр он съедает полностью.
 * Определить за сколько подходов Вовочка съест все фрукты в корзине.
 * Напишите свой класс кучи, реализующий очередь с приоритетом.
 * Формат входных данных. Вначале вводится n - количество фруктов и строка с целочисленными массами фруктов через пробел.
 * Затем в отдельной строке K - "грузоподъемность".
 * Формат выходных данных. Неотрицательное число - количество подходов к корзине.
 */

using System;
using System.Collections.Generic;

namespace ADS
{
    public class GreedyVovochka
    {
        public static void Main()
        {
            int n = Convert.ToInt32(Console.ReadLine());

            var fStr = Console.ReadLine().Split(' ');
            var fs = new int[n];
            for (int i = 0; i < n; ++i) fs[i] = Convert.ToInt32(fStr[i]);

            int k = Convert.ToInt32(Console.ReadLine());

            var heap = new BinaryHeap(fs);
            var stack = new Stack<int>();
            int count = 0;
            while (!heap.IsEmpty())
            {
                int w = k;
                while (!heap.IsEmpty() && heap.Peek() <= w)
                {
                    int max = heap.Remove();
                    w -= max;
                    stack.Push(max);
                }
                while (stack.Count > 0)
                {
                    int val = stack.Pop();
                    if (val > 1) heap.Add(val / 2);
                }
                count++;
            }

            Console.WriteLine(count);
            Console.ReadLine();
        }
    }
}