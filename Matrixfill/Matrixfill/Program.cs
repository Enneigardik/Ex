using System;
using System.Collections.Generic;

namespace Matrixfill
{
    class Program
    {
        static int[,] max;
        static Stack<int[]> stack = new Stack<int[]>();
        static void Main(string[] args)
        {
            int p = 10;
            max = new int[p, p];
            Console.WriteLine("Изначальное");
            Create(p);
            Show(max);
            Schet(max);
            Console.WriteLine("Заливка");
            fill(2, 2, max);
            Show(max);
            Schet(max);
            Console.WriteLine("Непонятные стеки");
            stackk(2, 2, max);
            Show(max);
            Schet(max);
        }
        public static void Create(int p)
        {
            max = new int[p, p];
            Random hate = new Random();
            for (int i = 0; i < p; i++)
            {
                for (int j = 0; j < p; j++)
                {
                    if (hate.Next(0, 4) == 1)
                    {
                        max[i, j] = 1;
                    }
                }
            }

        }

        public static void Show(int[,] max)
        {
            for (int i = 0; i < max.GetLength(0); i++)
            {
                for (int j = 0; j < max.GetLength(0); j++)
                {
                    Console.Write(Convert.ToString(max[i, j]) + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
        public static void fill(int x, int y, int[,] max)
        {
            const int c = 2;
            if (x >= 0 && y >= 0 && x < max.GetLength(0) && y < max.GetLength(0) && max[x, y] == 0)
            {
                max[x, y] = c;
                fill(x + 1, y, max);
                fill(x - 1, y, max);
                fill(x, y + 1, max);
                fill(x, y - 1, max);
            }

        }

        public static void stackk(int x, int y, int[,] max)
        {
            const int c = 2;
            stack.Push(new[] { x, y });
            while(stack.Count>0)
            {
                int[] st = stack.Pop();
                if (st[0] >= 0 && st[1] >= 0 && st[0] < max.GetLength(0) &&
                    st[1] < max.GetLength(0) &&
                    max[st[0], st[1]] == 0)
                {
                    max[st[0], st[1]] = c;
                    stack.Push(new[] { st[0], st[1] - 1 });
                    stack.Push(new[] { st[0] - 1, st[1] });
                    stack.Push(new[] { st[0], st[1] + 1 });
                    stack.Push(new[] { st[0] + 1, st[1] });
                }
            }
        }
       
            static void Schet(int[,] matrix)
        {
            int a1 = 0;
            int b2 = 0;
            int c0 = 0;
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(0); j++)
                {
                    if (matrix[i, j] == 1)
                    {
                        a1++;
                    }
                    if (matrix[i, j] == 2)
                    {
                        b2++;
                    }
                    if (matrix[i, j] == 0)
                    {
                        c0++;
                    }
                }
            }

            Console.WriteLine("1 - " + a1);
            Console.WriteLine("2 - " + b2);
            Console.WriteLine("0 - " + c0);
            Console.WriteLine();
        }
    }
}
