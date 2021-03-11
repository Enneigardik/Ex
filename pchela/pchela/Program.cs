using System;
using System.Collections.Generic;
namespace Galko
{
    public class Keyval
    {
        public int x;
        public int y;
        public Keyval(int a, int b)
        {
            x = a;
            y = b;
        }
    }
    public class MyComp : IComparer<Keyval>
    {
        public int Compare(Keyval l, Keyval r)
        {
            return l.y < r.y ? -1 : 1;
        }
    }

    class MyVector<T>
    {
        public int Count => size;
        public bool IsEmpty => size == 0;

        private int size = 0;
        private T[] data = new T[0];
        
        public T this[int n]
        {
            get
            {
                if (n < 0 || n > size)
                    throw new IndexOutOfRangeException("Я ЗАПРЕЩАЮ ВАМ ТУТ ТРОГАТЬ");
                return data[n];
            }
            set
            {

                if (n < 0 || n > size)
                    throw new IndexOutOfRangeException("Я ЗАПРЕЩАЮ ВАМ ТУТ ТРОГАТЬ");
                data[n] = value;
            }
        }


        public void Add(T x)
        {
            Resize(++size);
            data[size - 1] = x;
        }

        public void Insert(int x, T numer)
        {
            if (x < 0)
                throw new IndexOutOfRangeException("А как");
            if (x > size)
            {
                Resize(x + 1);
                data[x] = numer;
            }
            else
            {
                size++;
                Resize(size);
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
                for (int i = numer+1; i < size; i++)
                    data[i - 1] = data[i];
                Resize(--size);
                return t;
            }
            return default;
        }

        public T Last() =>
            size != 0 ? data[size - 1] : default;

        public T First() =>
            size != 0 ? data[0] : default;

        public void Clear()
        {
            size = 0;
            data = new T[0];
        }
        public bool Contains(T item) =>
            IndexOf(item) != -1;
        public int IndexOf(T item)
        {
            for (int i=0; i<size; i++)
                if (object.Equals(item, data[i]))
                    return i;
            return -1;
        }
        public IEnumerator<T> GetEnumerator() =>
            data.GetEnumerator() as IEnumerator<T>;

        public void ForEach(Action<T> action)
        {
            for(int i = 0; i < size; i++)
                action(data[i]);
        }
        public T Find(Predicate<T> match)
        {
            for (int i = 0; i < size; i++)
                if (match(data[i]))
                    return data[i];
            return default;
        }
        public int FindIndex(Predicate<T> match)
        {
            for (int i = 0; i < size; i++)
                if (match(data[i]))
                    return i;
            return -1;
        }
        private void Resize(int newSize)
        {
            newSize = (int) Math.Pow(2, Math.Ceiling(Math.Log2(newSize)));
            if (newSize != data.Length)
                Array.Resize(ref data, newSize);  
        }
    }


    class Program
    {
        delegate int g(int a, int b);
        delegate int f(int x);
        public static MyVector<int> hate = new MyVector<int>();
        static void Main(string[] args)
        {
            hate.Add(1);
            hate.Insert(0, 2);
            hate.Add(4);
            hate.ForEach(i => Console.Write(i + " "));
            hate.Add(5);
            hate.Add(8);
            Console.WriteLine();
            hate.ForEach(i => Console.Write(i + " "));
            Console.WriteLine(hate.Contains(1));
            //f sq = x => x * x;
            //Console.WriteLine(sq(5));
            //g sum = (x, y) => x + y;
            //Console.WriteLine(sum(4, 5));

            //System.Action<int> aa = null;
            //aa = x => Console.WriteLine(x);
            //aa(5);

            //System.Func<int, int, int> ff = null;
            //ff = (a, b) =>
            //  {
            //      if (a < b) return a + b;
            //      else return a * a + b * b;

            //  };
            //Console.WriteLine(ff(4, 5));

            //Func<int, int> fip = null;
            //fip = x => x > 1 ? fip(x - 1) + fip(x - 2) : x;
            //Console.WriteLine(fip(10));



            //var m = new Keyval[5];
            //m[0] = new Keyval(2, 5);
            //m[1] = new Keyval(4, 4);
            //m[2] = new Keyval(6, 3);
            //m[3] = new Keyval(8, 2);
            //m[4] = new Keyval(10, 1);
            ////Array.Sort(m, new MyComp());

            //Array.Sort(m, (a, b) => a.y.CompareTo(b.y));
            //foreach (var l in m)
            //{
            //    Console.WriteLine(l.x + " " + l.y);
            //}

            //var lst = new List<int> { 0, 1, 2, 3, 4 };
            //Console.WriteLine(lst.FindIndex(x => x == 3));
            //lst.ForEach(x => Console.WriteLine(x));

            //hate.Add(2);
            //Console.Write(hate[0]);
            //hate.Insert(5, 5);
            //hate.Insert(-5, -5);
            //Console.WriteLine(hate[5]);
        }
    }
}
