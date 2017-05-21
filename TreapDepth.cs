using System;

namespace ADS
{
    public class TreapDepth
    {
        public static void Main()
        {
            int n = Convert.ToInt32(Console.ReadLine());
            int x, y;
            Node bst = null;
            Treap root = null;
            for (int i = 0; i < n; ++i)
            {
                var xy = Console.ReadLine().Split(' ');
                x = Convert.ToInt32(xy[0]);
                y = Convert.ToInt32(xy[1]);

                if (bst == null) bst = new Node { Key = x };
                else bst.Add(x);

                if (root == null)
                    root = new Treap(x, y);
                else
                    root = root.Add(x, y);
            }

            Console.WriteLine(Math.Abs(root.Height() - bst.Height()));
            Console.ReadLine();
        }
    }

    public class Treap
    {
        public int Key { get; set; }
        public int P { get; set; }
        public Treap Left { get; set; }
        public Treap Right { get; set; }

        public Treap(int key, int p)
        {
            Key = key;
            P = p;
        }

        public Treap(int key, int p, Treap left, Treap right)
            : this(key, p)
        {
            Left = left;
            Right = right;
        }

        public Treap Add(int key, int p)
        {
            Treap l, r;
            Split(key, out l, out r);
            var m = new Treap(key, p);
            return Merge(Merge(l, m), r);
        }

        public void Split(int key, out Treap left, out Treap right)
        {
            Treap newT = null;
            if (Key <= key)
            {
                if (Right == null)
                    right = null;
                else
                    Right.Split(key, out newT, out right);
                left = new Treap(Key, P, Left, newT);
            }
            else
            {
                if (Left == null)
                    left = null;
                else
                    Left.Split(key, out left, out newT);
                right = new Treap(Key, P, newT, Right);
            }
        }

        public static Treap Merge(Treap l, Treap r)
        {
            if (l == null) return r;
            if (r == null) return l;

            if (l.P > r.P)
            {
                var newR = Merge(l.Right, r);
                return new Treap(l.Key, l.P, l.Left, newR);
            }
            else
            {
                var newL = Merge(l, r.Left);
                return new Treap(r.Key, r.P, newL, r.Right);
            }
        }

        public int Height()
        {
            int leftH = Left != null ? Left.Height() : 0;
            int rightH = Right != null ? Right.Height() : 0;
            return Math.Max(leftH, rightH) + 1;
        }
    }

    public class Node
    {
        public int Key { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }

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

        public int Height()
        {
            int leftH = Left != null ? Left.Height() : 0;
            int rightH = Right != null ? Right.Height() : 0;
            return Math.Max(leftH, rightH) + 1;
        }
    }
}