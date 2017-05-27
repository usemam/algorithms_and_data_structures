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

    public class BinaryHeap
    {
        private int[] _data;

        private int _size;

        public BinaryHeap(int[] data)
        {
            _data = data;
            _size = data.Length;
            for (int i = _size / 2 - 1; i >= 0; --i) SiftDown(i);
        }

        public void Add(int val)
        {
            _data[_size++] = val;
            SiftUp(_size - 1);
        }

        public int Remove()
        {
            int result = _data[0];
            if (_size > 1)
            {
                _data[0] = _data[_size - 1];
                _size--;
                SiftDown(0);
            }
            else
                _size = 0;
            return result;
        }

        public int Peek()
        {
            return _data[0];
        }

        public bool IsEmpty()
        {
            return _size == 0;
        }

        private void Swap(int i, int j)
        {
            int tmp = _data[i];
            _data[i] = _data[j];
            _data[j] = tmp;
        }

        private void SiftDown(int i)
        {
            int left = i * 2 + 1;
            int right = i * 2 + 2;
            int largest = i;
            if (left < _size && _data[left] > _data[i]) largest = left;
            if (right < _size && _data[right] > _data[largest]) largest = right;
            if (largest != i)
            {
                Swap(i, largest);
                SiftDown(largest);
            }
        }

        private void SiftUp(int i)
        {
            while (i > 0)
            {
                int parent = (i - 1) / 2;
                if (_data[i] <= _data[parent]) return;
                Swap(i, parent);
                i = parent;
            }
        }
    }
}