using System;
using System.Collections.Generic;
using System.Text;

namespace Matrixfill
{
    public class MyStack<T>
    {
        private T[] data;
        public int Count;

       public MyStack()
        {
            Count = 0;
            data = new T[0];
        }
        public void Push(T x)
        {
            Array.Resize(ref data, ++Count);
            data[Count - 1] = x;
        }

        public T Pop()
        {
            if (Count != 0)
            {
                var t = data[Count-1];
                Array.Resize(ref data, --Count);
                return t;
            }
            return default;
        }

        public T Peek()
        {
            if (Count != 0)
            {
                return data[Count - 1];
            }
            else
            {
                return default(T);
            }
        }
        public bool isEmpty()
        {
            return Count == 0;
        }
        public void Clear()
        {
            Count = 0;
            data = new T[0];
        }
    }

}
