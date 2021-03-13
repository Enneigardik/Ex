using System;

namespace laba1
{
    class Program
    {
        
        static void Main(string[] args)
        {
            
        double functiya(double x) => 4 * x - 8 * Math.Sin(x) + 1;

        double proizvodnaya(double x) => 4 - 8 * Math.Cos(x);
        
        double dihotomy(double a, double b)
        {
            int c = 0;
            double epsilon,x;
            x = 0;
            epsilon = Math.Pow(10,-5);
            while(b-a > epsilon)
            {
                c++;
                x =(a+b)/2;
                if(functiya(x)*functiya(b)>=0)
                    b = x;
                else
                    a = x;
            }
            Console.WriteLine(c);
            return(x);
            }

        
        double prostayaIteracia(double x0, double x1)
        {
            
            double t = -1/proizvodnaya(x1);
            int c = 0;
            double x = 1;
            double epsilon = Math.Pow(10,-5);
            for(;;)
            {
                c++;
                x = x0 + t*functiya(x0);
                
                if(Math.Abs(x-x0) < epsilon) break;
                x0 = x;
            }
            Console.WriteLine(c);
            return(x);
        }
            Console.WriteLine("Метод Дитохомии");
            Console.WriteLine(dihotomy(-3, 2));
            Console.WriteLine(dihotomy(-2, 1));
            Console.WriteLine("Метод простой итерации");
            Console.WriteLine(prostayaIteracia(-3, 2));
            Console.WriteLine(prostayaIteracia(-1, 1));
        }
    }
}
