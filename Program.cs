using System;

namespace Laba1_Markaryan
{
    class Program
    {
        static double f(double x)
        {
            return x * x + 6 * x + 12;
        }

        static (double, double) Svenn(double x0, double t)
        {
            double xPrev = x0;
            double xCurr = x0 + t;

            if (f(xCurr) > f(x0))
            {
                t = -t;
                xCurr = x0 + t;
            }

            while (f(xCurr) < f(xPrev))
            {
                t *= 2;
                xPrev = xCurr;
                xCurr += t;
            }

            return (xCurr, xPrev);
        }

        static double Bisection(double a, double b, double epsilon)
        {
            while (Math.Abs(b - a) > epsilon)
            {
                double xMiddle = (a + b) / 2;
                double gradient = 2 * xMiddle + 6;
                if (gradient < 0) a = xMiddle;
                else b = xMiddle;
            }
            return (a + b) / 2;
        }

        static double Dichotomy(double a, double b, double epsilon, double delta)
        {
            while (Math.Abs(b - a) > epsilon)
            {
                double x1 = (a + b - delta) / 2;
                double x2 = (a + b + delta) / 2;
                if (f(x1) < f(x2)) b = x2;
                else a = x1;
            }
            return (a + b) / 2;
        }

        static double GoldenSection(double a, double b, double epsilon)
        {
            double phi = (1 + Math.Sqrt(5)) / 2;
            double resphi = 2 - phi;

            double x1 = a + resphi * (b - a);
            double x2 = b - resphi * (b - a);
            while (Math.Abs(b - a) > epsilon)
            {
                if (f(x1) < f(x2))
                {
                    b = x2;
                    x2 = x1;
                    x1 = a + resphi * (b - a);
                }
                else
                {
                    a = x1;
                    x1 = x2;
                    x2 = b - resphi * (b - a);
                }
            }
            return (a + b) / 2;
        }

        static void Main(string[] args)
        {
            double x0 = 1;
            double t = 2;
            double epsilon = 1e-6;

            var interval = Svenn(x0, t);
            Console.WriteLine($"Svenn method: interval = [{interval.Item1}, {interval.Item2}]");

            double xMinBisection = Bisection(interval.Item1, interval.Item2, epsilon);
            Console.WriteLine($"Bisection method: x_min = {xMinBisection}");

            double xMinDichotomy = Dichotomy(interval.Item1, interval.Item2, epsilon, 1e-8);
            Console.WriteLine($"Dichotomy method: x_min = {xMinDichotomy}");

            double xMinGolden = GoldenSection(interval.Item1, interval.Item2, epsilon);
            Console.WriteLine($"Golden Section method: x_min = {xMinGolden}");
            Console.ReadKey();
        }
    }
}
