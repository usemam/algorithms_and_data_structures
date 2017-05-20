using System;
using System.Collections.Generic;
using System.Text;

namespace ADS
{
    public class Lists2
    {
        public static void Main()
        {
            var str = Console.ReadLine();
            var result = new StringBuilder();
            var stack = new Stack<char>();

            foreach (var c in str)
            {
                if (IsOpening(c))
                {
                    result.Append(c);
                    stack.Push(c);
                }
                else if (stack.Count == 0)
                {
                    result.Insert(0, Matching(c));
                    result.Append(c);
                }
                else if (stack.Peek() == Matching(c))
                {
                    result.Append(c);
                    stack.Pop();
                }
                else
                {
                    Console.WriteLine("IMPOSSIBLE");
                    return;
                }
            }

            while (stack.Count > 0)
                result = result.Append(Matching(stack.Pop()));

            Console.WriteLine(result.ToString());
            Console.ReadLine();
        }

        private static bool IsOpening(char c)
        {
            return c == '(' || c == '{' || c == '[';
        }

        private static char Matching(char c)
        {
            switch (c)
            {
                case '{': return '}';
                case '}': return '{';
                case '(': return ')';
                case ')': return '(';
                case '[': return ']';
                case ']': return '[';
                default: throw new NotSupportedException();
            }
        }
    }
}