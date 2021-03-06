using System;
using System.Collections.Generic;

namespace SAOD4
{
	public class ListNode<T> 
	{
		public T Value;
		public ListNode<T> Next { get; set; }
		public ListNode(T value)
		{
			Value = value;
		}
	}
	public class YourLinkedList<T> 
	{
		public int Count { private set; get; }
		private ListNode<T> first;
		public T First => Count > 0 ? first.Value : default;
		public T Last => Count > 0 ? this[Count - 1] : default;

		public T this[int i]
		{
			get
			{
				if (i < 0 || i >= Count)
					throw new IndexOutOfRangeException("Индекс вышел за границы списка: i = " + i + "; Count = " + Count);
				return GetNode(i).Value;
			}
			set
			{
				if (i < 0 || i >= Count)
					throw new IndexOutOfRangeException("Индекс вышел за границы списка: i = " + i + "; Count = " + Count);
				GetNode(i).Value = value;
			}
		}

		public void AddFirst(T element)
		{
			var node = new ListNode<T>(element);
			if (Count > 0)
				node.Next = first;
			first = node;
			Count++;
		}
		public void AddLast(T element)
		{
			var node = new ListNode<T>(element);
			if (Count > 0)
				GetNode(Count - 1).Next = node;
			else
				first = node;
			Count++;
		}
		public void Insert(int index, T element)
		{
			if (index == Count) AddLast(element);
			else if (index == 0) AddFirst(element);
			else
			{
				var next = GetNode(index);
				var prev = GetNode(index - 1);
				var node = new ListNode<T>(element);
				node.Next = next;
				prev.Next = node;
				Count++;
			}
		}
		public void RemoveAt(int index)
		{
			var node = GetNode(index);
			if (Count == 1)
				first = null;
			else if (first == node)
				first = node.Next;
			else
				GetNode(index - 1).Next = node.Next;
			Count--;
		}
		public int IndexOf(T element)
		{
			var cur = first;
			if (object.Equals(cur.Value, element)) return 0;
			for (var i = 1; i < Count; i++)
			{
				cur = cur.Next;
				if (object.Equals(cur.Value, element)) return i;
			}
			return -1;
		}
		public void Clear()
		{
			first = null;
			Count = 0;
		}

		public T[] ToArray()
		{
			var array = new T[Count];
			if (Count == 0) return array;
			var el = first;
			array[0] = el.Value;
			for (var i = 1; i < Count; i++)
			{
				el = el.Next;
				array[i] = el.Value;
			}
			return array;
		}
		public bool Contains(T element) =>
			IndexOf(element) != -1;
		public IEnumerator<T> GetEnumerator() =>
			ToArray().GetEnumerator() as IEnumerator<T>;

		private ListNode<T> GetNode(int index)
		{
			var cur = first;
			for (var j = 0; j < index; j++) cur = cur.Next;
			return cur;
		}
	}
	class Programm
	{
		static void Main(string[] args)
		{
			var list = new YourLinkedList<int>();
			list.AddLast(9);
			list.AddLast(1);
			list.AddLast(2);
			list.AddLast(3);
			for (int i = 0; i < list.Count; i++)
			{
				Console.Write(list[i] + "=>");
			}
			Console.WriteLine("null");
		}

	}
}
