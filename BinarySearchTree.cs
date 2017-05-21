using System;

namespace ADS
{
    public class BinarySearchTree
    {
        public static void Main()
        {
            int n = Convert.ToInt32(Console.ReadLine());
            var tree = ReadTree(n);
            tree.Print();
        }

        private static Node ReadTree(int n)
        {
            Node root = null;
            int i = 0, val = 0, dec = 1;
            bool isNegative = false;
            string str = Console.ReadLine();
            for (int j = 0; j < str.Length && i < n; ++j)
            {
                char c = str[j];

                if (c == ' ')
                {
                    root = AddToTree(root, val, isNegative);
                    val = 0;
                    dec = 1;
                    isNegative = false;
                    i++;
                }
                else if (c == '-')
                {
                    isNegative = true;
                }
                else if (c >= '0' && c <= '9')
                {
                    val += (c - '0') * dec;
                    dec *= 10;
                }
            }

            if (i < n) root = AddToTree(root, val, isNegative);

            return root;
        }

        private static Node AddToTree(Node root, int val, bool isNegative)
        {
            if (isNegative) val = -val;
            if (root == null)
                root = new Node { Key = val };
            else
                root.Add(val);

            return root;
        }

        public class Node
        {
            public int Key { get; set; }
            public Node Left { get; set; }
            public Node Right { get; set; }

            public void Print()
            {
                if (Left != null) Left.Print();
                if (Right != null) Right.Print();
                Console.Write("{0} ", Key);
            }

            public void Add(int val)
            {
                if (Key > val)
                {
                    if (Left == null)
                        Left = new Node { Key = val };
                    else
                        Left.Add(val);
                }
                else
                {
                    if (Right == null)
                        Right = new Node { Key = val };
                    else
                        Right.Add(val);
                }
            }
        }
    }
}