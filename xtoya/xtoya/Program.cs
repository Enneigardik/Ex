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
