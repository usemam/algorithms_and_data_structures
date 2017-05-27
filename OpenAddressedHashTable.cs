/*
 * Реализуйте структуру данных типа “множество строк” на основе динамической хеш-таблицы с открытой адресацией.
 * Хранимые строки непустые и состоят из строчных латинских букв. Начальный размер таблицы должен быть равным 8-ми.
 * Перехеширование выполняйте в случае, когда коэффициент заполнения таблицы достигает 3/4.
 * Структура данных должна поддерживать операции добавления строки в множество,
 * удаления строки из множества и проверки принадлежности данной строки множеству.
 * Для разрешения коллизий используйте квадратичное пробирование.
 * i-ая проба - g(k,i)=g(k,i−1)+i*(mod m). m - степень двойки. 
 * Каждая строка входных данных задает одну операцию над множеством.
 * Запись операции состоит из типа операции и следующей за ним через пробел строки, над которой проводится операция.
 * Тип операции  – один из трех символов:
 * +  означает добавление данной строки в множество; 
 * -  означает удаление  строки из множества;  
 * ?  означает проверку принадлежности данной строки множеству.
 * При добавлении элемента в множество НЕ ГАРАНТИРУЕТСЯ, что он отсутствует в этом множестве.
 * При удалении элемента из множества НЕ ГАРАНТИРУЕТСЯ, что он присутствует в этом множестве. 
 * Программа должна вывести для каждой операции одну из двух строк OK или FAIL, в зависимости от того,
 * встречается ли данное слово в нашем множестве. 
 */

using System;
using System.Collections.Generic;

namespace ADS
{
    public class OpenAddressedHashTable
    {
        public static void Main()
        {
            var output = new List<string>();
            var table = new HashTable<string>(new StringHasher());

            string line;
            while (!string.IsNullOrEmpty(line = Console.ReadLine()))
            {
                var sp = line.Split(' ');
                bool result = false;
                switch (sp[0][0])
                {
                    case '+':
                        result = table.Add(sp[1]);
                        break;
                    case '-':
                        result = table.Delete(sp[1]);
                        break;
                    case '?':
                        result = table.Search(sp[1]);
                        break;
                }
                output.Add(result ? "OK" : "FAIL");
            }

            foreach (string s in output)
                Console.WriteLine(s);
        }
    }

    public interface IHasher<in T>
    {
        int Hash(T val);
    }

    public class StringHasher : IHasher<string>
    {
        public int Hash(string val)
        {
            int hash = 0;
            foreach (char c in val) hash = (hash * 7 + c) % 32768;
            return hash;
        }
    }

    public class HashTable<T>
    {
        private int _size;

        private Node[] _table;

        private readonly IHasher<T> _hasher;

        public HashTable(IHasher<T> hasher)
        {
            _hasher = hasher;
            _table = new Node[8];
            _size = 0;
        }

        private bool IsEmpty(Node node)
        {
            return node == null || node.IsDeleted;
        }

        private void Resize()
        {
            var newLen = _table.Length * 2;
            var newTable = new Node[newLen];
            foreach (Node node in _table)
            {
                if (IsEmpty(node)) continue;

                int hash = node.Hash, j = 0;
                while (j < newLen)
                {
                    int ix = (hash + j * j) % newLen;
                    if (IsEmpty(newTable[ix]))
                    {
                        newTable[ix] = node;
                        break;
                    }
                    j++;
                }
            }

            _table = newTable;
        }

        public bool Add(T val)
        {
            int hash = _hasher.Hash(val);
            var node = new Node { Hash = hash };
            int len = _table.Length, j = 0;

            while (j < len)
            {
                int ix = (hash + j * j) % len;
                var existing = _table[ix];
                if (IsEmpty(existing))
                {
                    _table[ix] = node;
                    _size++;
                    break;
                }
                if (existing.Hash == hash) return false;
                j++;
            }

            bool isOverflow = j == len;
            if (isOverflow || _size / (double)len >= 0.75) Resize();
            return !isOverflow || Add(val);
        }

        public bool Delete(T val)
        {
            int hash = _hasher.Hash(val);
            int len = _table.Length, j = 0;

            while (j < len)
            {
                int ix = (hash + j * j) % len;
                var node = _table[ix];
                if (node != null && !node.IsDeleted && node.Hash == hash)
                {
                    node.IsDeleted = true;
                    _size--;
                    return true;
                }
                j++;
            }

            return false;
        }

        public bool Search(T val)
        {
            int hash = _hasher.Hash(val);
            int len = _table.Length, j = 0;

            while (j < len)
            {
                int ix = (hash + j * j) % len;
                var node = _table[ix];
                if (node != null && !node.IsDeleted && node.Hash == hash)
                {
                    return true;
                }
                j++;
            }

            return false;
        }

        private class Node
        {
            public bool IsDeleted { get; set; }
            public int Hash { get; set; }
        }
    }
}