
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAOD6
{
    public class BadTree<T> where T : IComparable
    {
        public int TreeHeight { get; private set; }
        public int LeafsCount { get; private set; }
        public int NodesCount { get; private set; }

        public event EventHandler OnEdit;

        private BadTreeNode<T> root { get => _root; set { _root = value; UpdateState(); } }
        public BadTreeNode<T> _root;
        //BadTreeNode<int> rooot;
        public void Add(T value)
        {
            BadTreeNode<T> node = new BadTreeNode<T>(value);
            if (root == null)
                root = node;
            else
                Add(node, root);
        }
        private void Add(BadTreeNode<T> node, BadTreeNode<T> subroot)
        {
            if (subroot.Value.CompareTo(node.Value) <= 0)
            {
                if (subroot.Right == null)
                    subroot.Right = node;
                else
                    Add(node, subroot.Right);
            }
            else
            {
                if (subroot.Left == null)
                    subroot.Left = node;
                else
                    Add(node, subroot.Left);
            }
        }

        private int Heiht(BadTreeNode<T> p) => p == null ? 0 : p.Height;
        private int balance(BadTreeNode<T> p) => p == null ? 0 : Heiht(p.Left) - Heiht(p.Right);
        public T[] SymmetricBypass
        {
            get
            {
                sym.Clear();
                Symmetric(root);
                return sym.ToArray();
            }
        }
        private List<T> sym = new List<T>();
        private void Symmetric(BadTreeNode<T> node)
        {
            if (node == null) return;
            Symmetric(node.Left);
            sym.Add(node.Value);
            Symmetric(node.Right);
        }
        public void Clear() =>
            root = null;

        public T MaxValueIter() =>
            GetMaxOrMinValueIter(new Func<BadTreeNode<T>, BadTreeNode<T>>(node => node.Right));
        public T MinValueIter() =>
            GetMaxOrMinValueIter(new Func<BadTreeNode<T>, BadTreeNode<T>>(node => node.Right));

        public T MaxValueRec() =>
            GetMaxOrMinValueRec(new Func<BadTreeNode<T>, BadTreeNode<T>>(node => node.Right), root);
        public T MinValueRec() =>
            GetMaxOrMinValueRec(new Func<BadTreeNode<T>, BadTreeNode<T>>(node => node.Left), root);

        public bool Contains(T value) =>
            FindingValue(value, root);

        public void reorder_to_ideal() => root = makeIdeal(root);

        private BadTreeNode<T> makeIdeal(BadTreeNode<T> p)
        {
            var nodes = new List<BadTreeNode<T>>();
            store(nodes, p);
            return build_ideal(nodes, 0, nodes.Count - 1);
        }

        private void store(List<BadTreeNode<T>> n, BadTreeNode<T> p)
        {
            if (p == null)
                return;
            store(n, p.Left);
            n.Add(p);
            store(n, p.Right);
        }

        public string Print()
        {
            var res = ToStringHelper(root).Value;
            return res;
        }

        private KeyValuePair<int, string> ToStringHelper(BadTreeNode<T> n)
        {
            if (n == null)
                return new KeyValuePair<int, string>(1, "\n");

            var left = ToStringHelper(n.Left);
            var right = ToStringHelper(n.Right);

            var objString = n.Value.ToString();
            var stringBuilder = new StringBuilder();

            stringBuilder.Append(' ', left.Key - 1);
            stringBuilder.Append(objString);
            stringBuilder.Append(' ', right.Key - 1);
            stringBuilder.Append('\n');

            var i = 0;
            while (i * left.Key < left.Value.Length && i * right.Key < right.Value.Length)
            {
                stringBuilder.Append(left.Value, i * left.Key, left.Key - 1);
                stringBuilder.Append(' ', objString.Length);
                stringBuilder.Append(right.Value, i * right.Key, right.Key);
                ++i;
            }

            while (i * left.Key < left.Value.Length)
            {
                stringBuilder.Append(left.Value, i * left.Key, left.Key - 1);
                stringBuilder.Append(' ', objString.Length + right.Key - 1);
                stringBuilder.Append('\n');

                ++i;
            }

            while (i * right.Key < right.Value.Length)
            {
                stringBuilder.Append(' ', left.Key + objString.Length - 1);
                stringBuilder.Append(right.Value, i * right.Key, right.Key);
                ++i;
            }
            return new KeyValuePair<int, string>(left.Key + objString.Length + right.Key - 1, stringBuilder.ToString());
        }

        public BadTreeNode<T> Rrot(BadTreeNode<T> r)
        {
            var p = r.Left;
            r.Left = p.Right;
            p.Right = r;
            return p;
        }
        public BadTreeNode<T> Lrot(BadTreeNode<T> r)
        {
            var p = r.Right;
            r.Right = p.Left;
            p.Left = r;
            return p;
        }
        public BadTreeNode<T> BRrot(BadTreeNode<T> r)
        {
            var p = r.Left;
            r.Left = Lrot(p);
            return Rrot(r);
        }
        public BadTreeNode<T> BLrot(BadTreeNode<T> r)
        {
            var p = r.Right;
            r.Right = Rrot(p);
            return Lrot(r);
        }


        public int GetHeight()
        {
            return geth(root);
        }
        private int geth(BadTreeNode<T> p)
        {
            if (p == null)
                return 0;
            return 1 + Math.Max(geth(p.Left), geth(p.Right));
        }
        private BadTreeNode<T> build_ideal(List<BadTreeNode<T>> n, int a, int b)
        {
            if (a > b)
                return null;
            int m = (a + b) / 2;
            var p = n[m];
            p.Left = build_ideal(n, a, m - 1);
            p.Right = build_ideal(n, m + 1, b);
            return p;
        }

        private bool FindingValue(T value, BadTreeNode<T> node)
        {
            if (node == null) return false;
            if (object.Equals(node.Value, value)) return true;
            if (value.CompareTo(node.Value) > 0) return FindingValue(value, node.Right);
            else return FindingValue(value, node.Left);
        }
        private T GetMaxOrMinValueRec(Func<BadTreeNode<T>, BadTreeNode<T>> next, BadTreeNode<T> node)
        {
            if (node == null) return default;
            if (next(node) == null) return node.Value;
            return GetMaxOrMinValueRec(next, next(node));
        }
        private T GetMaxOrMinValueIter(Func<BadTreeNode<T>, BadTreeNode<T>> next)
        {
            if (root == null) return default;
            var t = root;
            while (next(t) != null)
                t = next(t);
            return t.Value;
        }
        private void UpdateState()
        {
            TreeHeight = root == null ? 0 : CalculatingHeight(1, root);
            LeafsCount = root == null ? 0 : CalculatingLeafsCount(root);
            NodesCount = root == null ? 0 : CalculatingNodesCount(1, root);
            OnEdit?.Invoke(null, null);
        }
        private void AddNode(T value, BadTreeNode<T> node)
        {
            if (value.CompareTo(node.Value) >= 0)
            {
                if (node.Right == null)
                    node.Right = new BadTreeNode<T>(value);
                else
                    AddNode(value, node.Right);
            }
            else
            {
                if (node.Left == null)
                    node.Left = new BadTreeNode<T>(value);
                else
                    AddNode(value, node.Left);
            }
        }
        private int CalculatingHeight(int cur, BadTreeNode<T> node)
        {
            if (node.Left == null && node.Right == null) return cur;
            else if (node.Left == null) return CalculatingHeight(cur + 1, node.Right);
            else if (node.Right == null) return CalculatingHeight(cur + 1, node.Left);
            else return Math.Max(CalculatingHeight(cur + 1, node.Left), CalculatingHeight(cur + 1, node.Right));
        }
        private int CalculatingNodesCount(int cur, BadTreeNode<T> node)
        {
            if (node.Left == null && node.Right == null) return cur;
            else if (node.Left == null) return cur + CalculatingNodesCount(1, node.Right);
            else if (node.Right == null) return cur + CalculatingNodesCount(1, node.Left);
            else return cur + CalculatingNodesCount(1, node.Left) + CalculatingNodesCount(1, node.Right);
        }
        private int CalculatingLeafsCount(BadTreeNode<T> node)
        {
            if (node.Left == null && node.Right == null) return 1;
            else if (node.Left == null) return CalculatingLeafsCount(node.Right);
            else if (node.Right == null) return CalculatingLeafsCount(node.Left);
            else return CalculatingLeafsCount(node.Left) + CalculatingLeafsCount(node.Right);
        }

    }
    //public class BNode
    //{
    //    public int item;
    //    public BNode right;
    //    public BNode left;

    //    public BNode(int item)
    //    {
    //        this.item = item;
    //    }
    //}

    public class BadTreeNode<T> where T : IComparable
    {
        public BadTreeNode<T> Left, Right;
        public T Value;
        public int Height;
        public BadTreeNode(T value)
        {
            Value = value;
            Height = 1;
        }
            
    }
    class Program
    {
        static void Main(string[] args)
        {
            //var list = new BadTree<int>();
            //list.Add(5);
            //list.Add(3);
            //list.Add(2);
            //list.Add(4);
            //list.Add(7);
            var list = new BadTree<int>();
            list.Add(4);
            list.Add(1);
            list.Add(9);
            list.Add(6);
            list.Add(10);
            list.Add(7);
            list.Add(5);
            Console.WriteLine(list.Print());
            Console.WriteLine();
            list._root = list.BLrot(list._root);
            Console.WriteLine(list.Print());

            //list.print();

            //Console.WriteLine(list.TreeHeight);
            //Console.WriteLine(string.Join(", ", list.SymmetricBypass.Select(num => num.ToString())));
            //var tree = new BadTree<int>();
            //var rnd = new System.Random(1);
            //var init = Enumerable.Range(1, 31).OrderBy(x => rnd.Next()).ToArray();
            //foreach (var i in init)
            //{
            //    tree.Add(i);
            //}
            //Console.WriteLine(tree.GetHeight());
            //tree.reorder_to_ideal();
            //Console.WriteLine(tree.GetHeight());

            //Console.WriteLine(list.Print());
            //list._root = list.Rrot(list._root);
            //Console.WriteLine(list.Print());
            //list._root = list.Lrot(list._root);
            //Console.WriteLine(list.Print());

        }
    }

}

