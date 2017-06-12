namespace ADS
{
    public interface IHasher<in T>
    {
        int Hash(T val);
    }

    /// <summary>
    /// Open-addressed dynamic hash-table.
    /// </summary>
    /// <typeparam name="T"></typeparam>
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