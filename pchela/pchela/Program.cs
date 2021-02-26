using System;
using System.Collections.Generic;

namespace vectora
{
    class MyVector<T>
    {
        private T[] data;
        public int size;
        public T this[int key]
        {
            get
            {
                return data[key];
            }
            set
            {
                data[key] = value;
            }
        }

        public int Count
        {
            get
            {
                return size;
            }
        }

        public void Add(T x)
        {
            Array.Resize(ref data, ++size);
            data[size - 1] = x;
        }
        public T Insert(T x, int numer)
        {
           
        }
        public T RemoveAt(int numer)
        {
            if (size != 0)
            {

                var t = data[numer];
                Array.Resize(ref data, --size);
                return t;
            }
            return default;
        }

        public T Last()
        {
            if (size != 0)
            {
                return data[size - 1];
            }
            else
            {
                return default(T);
            }
        }
        public T First()
        {
            if (size != 0)
            {
                return data[0];
            }
            else
            {
                return default(T);
            }
        }

        public void Clear()
        {
            size = 0;
            data = new T[0];
        }



    }

    class Program
    {

        public static MyVector<List<int>> vect = new MyVector<List<int>>();
        static void Main(string[] args)
        {
            List<int> AAA = new List<int>();


        }

    }
}

