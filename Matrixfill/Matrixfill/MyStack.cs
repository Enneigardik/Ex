using System;
using System.Collections.Generic;
namespace Galko
{
    class Program
    {
        public class Node
        {
            public int data;
            public Node next;
            public Node (int x)
            {
                this.data = x;
                this.next = null;
            }
        }
        public class SLList
        {
            public Node head;
            public Node tail;
            public int size = 0;
            public int this[int index]
            {
                get
                {
                    return GetValue(index);
                }
                set
                {

                }
            }
            public int GetValue(int p)
            {
                var curr = head;
                for(int i = 0; i<p && curr!=null; i++)
                {
                    curr = curr.next;
                }
                return curr.data;
            }
            public SLList()
            {
                head = null;
                tail = head;
                size = 0;

            }
            public IEnumerator<int> GetEnumerator()
            {
                for (int i=0; i<size;i++)
                {
                    return  GetValue(i);
                }
            }
            public void AddFirst(int x)
            {
                ++size;
                var newNode = new Node(x);
                if (head == null)
                {
                    //head = newNode;
                    //tail = head;
                }
                else
                {
                    newNode.next = head;
                    head = newNode;
                }
                public void AddLast(int x)
            {
                ++size;
                var newNode = new Node(x);
                if (head == null)
                {
                    head = newNode;
                    tail = head;
                }
                else
                {
                    var t = tail;
                    t.next = newNode;
                    tail = newNode;
                }
               // tail.next = newNode;
                //tail = newNode;
            }
        }
        static void Main(string[] args)
        {
            var list = new SLList();
            list.AddLast(9);
            list.AddLast(1);
            list.AddLast(2);
            list.AddLast(3);
            for (int i =0; i<list.size; i++)
            {
                Console.Write(list[i] + "=>");
            }
            Console.WriteLine("null");
        }
    }
}
