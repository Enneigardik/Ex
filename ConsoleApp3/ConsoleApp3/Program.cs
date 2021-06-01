using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using System.Diagnostics.CodeAnalysis;
using System.Collections;

namespace ConsoleApp9
{
    class Program
    {
        static void Main(string[] args)
        {
            string input_text = File.ReadAllText(@"big.txt");

            string text = input_text;
            //string text = " aa bb c aa b c aa b ";
            var str = new StringBuilder();
            //var d = new Dict<string, int>(7);
            var tree = new AVLTree<string, int>();
            foreach (var c in text)
            {
                if ((c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z'))
                {
                    str.Append(c);
                }
                else if (str.Length > 0)
                {
                    var key = str.ToString();
                    var node = tree.GetNode(key);
					if (node == null)
                        tree.Add(key, 1);
                    else
                        tree[key]++;
                    str.Clear();
                }

            }
            System.Diagnostics.Stopwatch watch = System.Diagnostics.Stopwatch.StartNew();
            Console.WriteLine(tree["the"]);

            watch.Stop();
            Console.WriteLine("time: " + watch.ElapsedMilliseconds);

        }
    }

    public class AVLTree<TKey, TValue> : IDictionary<TKey, TValue> where TKey : IComparable
    {
		public ICollection<TKey> Keys => GetNodes().Select(node => node.Key).ToArray();
		public ICollection<TValue> Values => GetNodes().Select(node => node.Value).ToArray();
        public int Count => GetNodes().Count;
		public bool IsReadOnly => false;
		
        private Node<TKey, TValue> root;

        public TValue this[TKey key] 
        {
            get => GetNode(key).Value;
            set => GetNode(key).Value = value;
        }

        public Node<TKey, TValue> GetNode(TKey key) =>
            GetNode(key, root);
        public bool ContainsKey(TKey key) =>
            GetNode(key, root) != null;

        public bool Remove(TKey key) => false;

        public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
        {
            var node = GetNode(key, root);
            value = node.Value;
            return node != null;
        }

        public void Add(KeyValuePair<TKey, TValue> item) =>
            root = Insert(root, item.Key, item.Value);
        public void Add(TKey key, TValue value) =>
            root = Insert(root, key, value);

        public void Clear() =>
            root = null;

        public bool Contains(KeyValuePair<TKey, TValue> item) =>
            GetNodes().Exists(node => Equals(node.Key, item.Key) && Equals(node.Value, item.Value));

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        { }

        public bool Remove(KeyValuePair<TKey, TValue> item) => false; 

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() =>
            GetEnumerator(root);
        IEnumerator IEnumerable.GetEnumerator() =>
            GetEnumerator(root);

        private Node<TKey, TValue> Insert(Node<TKey, TValue> p, TKey key, TValue value)
        {
            if (p == null)
                return new Node<TKey, TValue>(key, value);

            if (key.CompareTo(p.Key) < 0)
                p.Left = Insert(p.Left, key, value);
            else
                p.Right = Insert(p.Right, key, value);

            UpdateHeight(p);
            int b = GetBalancedHeight(p);
            if (b > 1 && key.CompareTo(p.Left.Key) < 0)
                return RotateRight(p);
            if (b < -1 && key.CompareTo(p.Right.Key) > 0)
                return RotateLeft(p);
            if (b > 1 && key.CompareTo(p.Left.Key) > 0)
            {
                p.Left = RotateLeft(p.Left);
                return RotateRight(p);
            }
            if (b < -1 && key.CompareTo(p.Right.Key) < 0)
            {
                p.Right = RotateRight(p.Right);
                return RotateLeft(p);
            }
            return p;
        }
        private int GetHeight(Node<TKey, TValue> p) =>
            p == null ? 0 : p.Height;
        private int GetBalancedHeight(Node<TKey, TValue> p) =>
            p == null ? 0 : GetHeight(p.Left) - GetHeight(p.Right);
        private void UpdateHeight(Node<TKey, TValue> p) =>
            p.Height = 1 + Math.Max(GetHeight(p.Left), GetHeight(p.Right));
        
        private Node<TKey, TValue> RotateRight(Node<TKey, TValue> node)
        {
            var nL = node.Left;
            if (nL == null) return nL;
            var t = nL.Right;

            nL.Right = node;
            node.Left = t;

            UpdateHeight(node);
            UpdateHeight(nL);

            return nL;
        }
        private Node<TKey, TValue> RotateLeft(Node<TKey, TValue> node)
        {
            var nR = node.Right;
            if (nR == null) return nR;
            var t = nR.Left;

            nR.Left = node;
            node.Right = t;

            UpdateHeight(node);
            UpdateHeight(nR);

            return nR;
        }
        private IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator(Node<TKey, TValue> cur)
		{
            if (cur != null)
			{
                yield return new KeyValuePair<TKey, TValue>(cur.Key, cur.Value);
                var enumLeft = GetEnumerator(cur.Left);
                var enumRight = GetEnumerator(cur.Right);
                while (enumLeft.MoveNext())
                    yield return enumLeft.Current;
                while (enumRight.MoveNext())
                    yield return enumRight.Current;
            }
		}
        private List<Node<TKey, TValue>> GetNodes()
        {
            var l = new List<Node<TKey, TValue>>();
            GetNodes(root, l);
            return l;
        }
        private Node<TKey, TValue> GetNode(TKey key, Node<TKey, TValue> cur)
        {
            if (cur == null) return null;
            if (Equals(key, cur.Key)) return cur;
            if (key.CompareTo(cur.Key) > 0)
                return GetNode(key, cur.Right);
            else
                return GetNode(key, cur.Left);
        }
        private void GetNodes(Node<TKey, TValue> cur, List<Node<TKey, TValue>> nodes)
        {
            if (cur != null)
            {
                nodes.Add(cur);
                GetNodes(cur.Left, nodes);
                GetNodes(cur.Right, nodes);
            }
        }
    }

    public class Node<TKey, TValue>
    {
        public Node<TKey, TValue> Left;
        public Node<TKey, TValue> Right;

        public TKey Key;
        public TValue Value;
        public int Height;

        public Node(TKey key, TValue value)
        {
            Key = key;
            Value = value;
            Height = 1;
        }
    }

    public class Dict<Tkey, Tvalue>
    {
        public struct Entry
        {
            public int HashCode;
            public int next;
            public Tkey key;
            public Tvalue value;
        }
        private int[] buckets;
        private Entry[] entries;
        private int freeList;
        private int freeCount;
        private int Count;
        private IEqualityComparer<Tkey> comp;
        public Dict(int capasity)
        {
            comp = EqualityComparer<Tkey>.Default;
            buckets = new int[capasity];
            entries = new Entry[capasity];
            for (int i = 0; i < capasity; i++)
                buckets[i] = -1;
            freeList = -1;

        }
        private void Resize()
        {
            Resize(Count * 2);
        }
        private void Resize(int newSize)
        {
            int[] newBuckets = new int[newSize];
            for (int i = 0; i < newBuckets.Length; i++)
                newBuckets[i] = -1;
            Entry[] newEntries = new Entry[newSize];
            Array.Copy(entries, 0, newEntries, 0, Count);

            for (int i = 0; i < Count; i++)
            {
                if (newEntries[i].HashCode >= 0)
                {
                    int bucket = newEntries[i].HashCode % newSize;
                    newEntries[i].next = newBuckets[bucket];
                    newBuckets[bucket] = i;
                }
            }
            buckets = newBuckets;
            entries = newEntries;
        }
        public void Add(Tkey key, Tvalue value)
        {
            int hashCode = comp.GetHashCode(key) & 0x7FFFFFFF;
            int targetBucket = hashCode % buckets.Length;

            for (int i = buckets[targetBucket]; i >= 0; i = entries[i].next)
            {
                if (entries[i].HashCode == hashCode && comp.Equals(entries[i].key,
                key))
                {
                    entries[i].value = value;
                    return;
                }
            }

            int index;
            if (freeCount > 0)
            {
                index = freeList;
                freeList = entries[index].next;
                freeCount--;
            }
            else
            {
                if (Count == entries.Length)
                {
                    Resize();
                    targetBucket = hashCode % buckets.Length;
                }
                index = Count;
                Count++;
            }
            entries[index].HashCode = hashCode;
            entries[index].next = buckets[targetBucket];
            entries[index].key = key;
            entries[index].value = value;
            buckets[targetBucket] = index;
        }

        private int FindEntry(Tkey key)
        {
            if (buckets != null)
            {
                int hashCode = comp.GetHashCode(key) & 0x7FFFFFFF;
                for (int i = buckets[hashCode % buckets.Length]; i >= 0; i = entries[i].next)
                {
                    if (entries[i].HashCode == hashCode && comp.Equals(entries[i].key, key))
                        return i;
                }
            }
            return -1;
        }
        public Tvalue this[Tkey key]
        {
            get
            {
                int i = FindEntry(key);
                if (i >= 0)
                {
                    return entries[i].value;
                }
                return default(Tvalue);
            }
            set
            {
                Add(key, value);
            }
        }

        public bool ContainsKey(Tkey key)
        {
            return FindEntry(key) >= 0;
        }

        public void Print()
        {
            Console.WriteLine("Index\tBuckets\t\tEntries");
            for (int i = 0; i < buckets.Length; i++)
            {
                Console.Write(i + "\t" + buckets[i]);
                Console.Write("\t\tKey" + entries[i].key);
                Console.WriteLine(entries[i].next <= 0 ? "" : "->" + entries[i].next);
            }
        }
    }
}