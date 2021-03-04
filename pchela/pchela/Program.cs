using System;
using System.Collections.Generic;

namespace vectora
{
    class MyVector<T>
    {
        int size = 0;
        T[] data = new T[0];
        public T this[int n]
        {
            get
            {
                if (n < 0 || n > size)
                {
                    throw new IndexOutOfRangeException("Я ЗАПРЕЩАЮ ВАМ ТУТ ТРОГАТЬ");
                }
                return data[n];
            }
        }

        public int Count()
        {
            return size;
        }
        public bool IsEmpty()
        {
            return size == 0;
        }

        public void Add(T x)
        {
            Array.Resize(ref data, ++size);
            data[size - 1] = x;
        }

        public void Insert(int x, T numer)
        {
            if (x < 0)
            {
                throw new IndexOutOfRangeException("А как");
            }
            if (x > size)
            {
                size += x;
                Array.Resize(ref data, size);
                data[x] = numer;
            }
            else if (x == size)
            {
                data[x] = numer;
            }
            else if (x < size)
            {
                size++;
                Array.Resize(ref data, size);
                for (int i = size - 1; i > x; i--)
                    data[i] = data[i - 1];
                data[x] = numer;
            }
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
        
        public T Lastl()
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

        public static MyVector<int> hate = new MyVector<int>();
        static void Main(string[] args)
        {
            hate.Add(2);
            Console.Write(hate[0]);
            hate.Insert(5, 5);
            hate.Insert(-5, -5);
            Console.WriteLine(hate[5]);
           
        }

    }
}

