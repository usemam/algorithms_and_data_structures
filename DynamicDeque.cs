﻿/*
 * Реализуйте дек с динамическим зацикленным буфером.
 * Для тестирования дека на вход подаются команды. 
 * В первой строке количество команд. Затем в каждой строке записана одна команда. 
 * Каждая команда задаётся как 2 целых числа: a b.
 * a = 1 - push front,
 * a = 2 - pop front,
 * a = 3 - push back, 
 * a = 4 - pop back.
 * 
 * Если дана команда pop*, то число b - ожидаемое значение. Если команда pop вызвана для пустой структуры данных, то ожидается “-1”.
 * Требуется напечатать YES, если все ожидаемые значения совпали. Иначе, если хотя бы одно ожидание не оправдалось, то напечатать NO.
 */

using System;

namespace ADS
{
    public class DynamicDeque
    {
        public static void Main()
        {
            int n = Convert.ToInt32(Console.ReadLine());

            bool all = CheckCommands(n);
            Console.WriteLine(all ? "YES" : "NO");
            Console.ReadLine();
        }

        private static bool CheckCommands(int n)
        {
            bool result = true;
            var deq = new Deque();
            for (int i = 0; i < n; i++)
            {
                var tuple = Console.ReadLine().Split(' ');
                var cmd = (Command) Convert.ToInt32(tuple[0]);
                var val = Convert.ToInt32(tuple[1]);

                switch (cmd)
                {
                    case Command.PushFront:
                        deq.PushFront(val);
                        break;
                    case Command.PopFront:
                        result &= deq.PopFront() == val;
                        break;
                    case Command.PushBack:
                        deq.PushBack(val);
                        break;
                    case Command.PopBack:
                        result &= deq.PopBack() == val;
                        break;
                }
            }

            return result;
        }

        public enum Command
        {
            PushFront = 1,
            PopFront = 2,
            PushBack = 3,
            PopBack = 4
        }

        public class Deque
        {
            private Node _head;

            private Node _tail;

            public void PushFront(int val)
            {
                if (_head == null)
                {
                    _head = _tail = new Node {Data = val};
                }
                else
                {
                    var node = new Node {Data = val, Next = _head};
                    _head.Prev = node;
                    _head = node;
                }
            }

            public void PushBack(int val)
            {
                if (_tail == null)
                {
                    _head = _tail = new Node { Data = val };
                }
                else
                {
                    var node = new Node {Data = val, Prev = _tail};
                    _tail.Next = node;
                    _tail = node;
                }
            }

            public int PopFront()
            {
                if (_head == null) return -1;

                var result = _head.Data;
                _head = _head.Next;
                if (_head == null) _tail = null;
                return result;
            }

            public int PopBack()
            {
                if (_tail == null) return -1;

                var result = _tail.Data;
                _tail = _tail.Prev;
                if (_tail == null) _head = null;
                return result;
            }

            private class Node
            {
                public int Data { get; set; }
                
                public Node Next { get; set; }

                public Node Prev { get; set; }
            }
        }
    }
}