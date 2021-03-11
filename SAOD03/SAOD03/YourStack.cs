using System;

namespace SAOD03
{
	public class YourStack<T>
	{
		public int Count => count;
		public bool IsEmpty => count == 0;

		private T[] array;
		private int count = 0;

		public YourStack(int length)
		{
			array = new T[length];
		}

		public T Peek()
		{
			if (count == 0) throw new NullReferenceException("Стек пуст");
			return array[count - 1];
		}
		public T Pop()
		{
			if (count == 0) throw new NullReferenceException("Стек пуст");
			count--;
			return array[count];
		}
		public void Push(T element)
		{
			if (count == array.Length) throw new NullReferenceException("Стек переполнен");
			array[count] = element;
			count++;
		}

		public void Resize(int newLength)
		{
			if (count > newLength) throw new NullReferenceException("Стек переполнен");
			var t = new T[newLength];
			for (var i = 0; i < newLength; i++)
				t[i] = array[i];
			array = t;
		}

		public void Clear() =>
			count = 0;

		public YourStack<T> Clone()
		{
			var t = new YourStack<T>(array.Length);
			for (var i = 0; i < count; i++)
			{
				t.array[i] = array[i];
				t.count = count;
			}
			return t;
		}
	}
}
