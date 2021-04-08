using System;
using System.Collections.Generic;
using System.Linq;

namespace xtoya
{

    public class Node
    {
        public int data;
        public Node left { get; set; }
        public Node right { get; set; }

        public Node(int x)
        {
            this.data = x;
        }
    }

    class Tree
    {
        public int Size { private set; get; }
        Node root;
        void Clear()
        {

        }
        //T MaxValueItr()
        //{

        //}
        public void Add(int x)
        {
            if (root == null)
                root = new Node(x);
            else
                Add(x, root);
        }
        private void Add(int x, Node p)
        {
            if (x < p.data)
            {
                if (p.left == null)
                    p.left = new Node(x);
                else
                    Add(x, p.left);
            }
            else if (x > p.data)
            {
                if (p.right == null)
                    p.right = new Node(x);
                else
                    Add(x, p.right);

            }
        }
        public void print()
        {
            print(root, 0);
        }
        private void print(Node p, int h)
        {

            if (p.right != null)
                print(p.right, h + 1);
            for (int i = 0; i < h; i++)
                Console.Write(" ");
            Console.WriteLine(p.data);

            if (p.left != null)
                print(p.left, h + 1);
        }
        public int GetHeight()
        {
            return geth(root);
        }
        private int geth(Node p)
        {
            if (p == null)
                return 0;
            return 1 + Math.Max(geth(p.left), geth(p.right));
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            //var list = new Tree();
            //list.Add(5);
            //list.Add(8);
            //list.Add(3);
            //list.Add(1);
            //list.Add(4);
            //list.Add(6);
            //list.Add(9);
            //list.print();
            //Console.WriteLine();
            //Console.WriteLine(list.GetHeight());
            var tree = new Tree();
            var rnd = new System.Random(1);
            var init = Enumerable.Range(0, 10_000_000).OrderBy(x => rnd.Next()).ToArray();
            foreach (var i in init)
            {
                tree.Add(i);
            }
            Console.WriteLine(tree.GetHeight());
            //for (int i = 0; i < init.Length; i++)
            //{
            //    Console.WriteLine(init[i]+" ");

            //}//random shafle list
            //List<int> llist = new List<int>();
            //for (int i = 1; i != 32; i++)
            //{
            //    llist.Add(i);
            //}
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;

namespace SAOD6
{
	public class BadTree<T> where T : IComparable
	{
		public int TreeHeight { get; private set; }
		public int LeafsCount { get; private set; }
		public int NodesCount { get; private set; }

		public event EventHandler OnEdit;

		private BadTreeNode<T> root { get => _root; set { _root = value; UpdateState(); } }
		private BadTreeNode<T> _root;
		BadTreeNode <int> rooot;
		public void Add(T value)
		{
			if (root == null)
				root = new BadTreeNode<T>(value);
			else
				AddNode(value, root);
			UpdateState();
		}
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
	public class BNode
	{
		public int item;
		public BNode right;
		public BNode left;

		public BNode(int item)
		{
			this.item = item;
		}
	}
	public static class BTreePrinter
	{
		class NodeInfo
		{
			public BNode Node;
			public string Text;
			public int StartPos;
			public int Size { get { return Text.Length; } }
			public int EndPos { get { return StartPos + Size; } set { StartPos = value - Size; } }
			public NodeInfo Parent, Left, Right;
		}

		public static void Print(this BNode root, string textFormat = "0", int spacing = 1, int topMargin = 2, int leftMargin = 2)
		{
			if (root == null) return;
			int rootTop = Console.CursorTop + topMargin;
			var last = new List<NodeInfo>();
			var next = root;
			for (int level = 0; next != null; level++)
			{
				var item = new NodeInfo { Node = next, Text = next.item.ToString(textFormat) };
				if (level < last.Count)
				{
					item.StartPos = last[level].EndPos + spacing;
					last[level] = item;
				}
				else
				{
					item.StartPos = leftMargin;
					last.Add(item);
				}
				if (level > 0)
				{
					item.Parent = last[level - 1];
					if (next == item.Parent.Node.left)
					{
						item.Parent.Left = item;
						item.EndPos = Math.Max(item.EndPos, item.Parent.StartPos - 1);
					}
					else
					{
						item.Parent.Right = item;
						item.StartPos = Math.Max(item.StartPos, item.Parent.EndPos + 1);
					}
				}
				next = next.left ?? next.right;
				for (; next == null; item = item.Parent)
				{
					int top = rootTop + 2 * level;
					Print(item.Text, top, item.StartPos);
					if (item.Left != null)
					{
						Print("/", top + 1, item.Left.EndPos);
						Print("_", top, item.Left.EndPos + 1, item.StartPos);
					}
					if (item.Right != null)
					{
						Print("_", top, item.EndPos, item.Right.StartPos - 1);
						Print("\\", top + 1, item.Right.StartPos - 1);
					}
					if (--level < 0) break;
					if (item == item.Parent.Left)
					{
						item.Parent.StartPos = item.EndPos + 1;
						next = item.Parent.Node.right;
					}
					else
					{
						if (item.Parent.Left == null)
							item.Parent.EndPos = item.StartPos - 1;
						else
							item.Parent.StartPos += (item.StartPos - 1 - item.Parent.EndPos) / 2;
					}
				}
			}
			Console.SetCursorPosition(0, rootTop + 2 * last.Count - 1);
		}

		private static void Print(string s, int top, int left, int right = -1)
		{
			Console.SetCursorPosition(left, top);
			if (right < 0) right = left + s.Length;
			while (Console.CursorLeft < right) Console.Write(s);
		}
	}
	public class BadTreeNode<T> where T : IComparable
	{
		public BadTreeNode<T> Left, Right;
		public T Value;
		public BadTreeNode(T value) =>
			Value = value;
	}
    class Program
    {
        static void Main(string[] args)
        {
			var list = new BadTree<int>();
			list.Add(5);
			list.Add(8);
			list.Add(3);
			list.Add(1);
			list.Add(4);
			list.Add(6);
			list.Add(9);
			//list.print();
			Console.WriteLine();
			Console.WriteLine(list.TreeHeight);
			Console.WriteLine(string.Join(", ", list.SymmetricBypass.Select(num => num.ToString())));
		}
	}

}



