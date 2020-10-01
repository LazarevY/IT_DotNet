using System;
using System.Diagnostics;
using System.IO;
using Task02Lib;

namespace Task02
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length != 3)
            {
                Console.WriteLine("Command must be like this:A B N" +
                                  "\nA and B - double values, A <= B, N - whole number");
                Environment.Exit(-1);
            }

            Func<double, double> fX = x => Math.Sin(x) * Math.Pow(Math.Cos(x), 2) * Math.Pow(Math.Exp(x), 3);

            double xStart = Double.Parse(args[0]);
            double xEnd = Double.Parse(args[1]);
            int n = int.Parse(args[2]);

            if (xStart > xEnd)
            {
                Console.WriteLine("B value must be more or equal than A!");
                Environment.Exit(-1);
            }

            var table = Lib.FunctionValues(fX, xStart, xEnd, (xEnd - xStart) / n);

            var strTab = Lib.FunctionValuesToString(table,
                "{0,10:######0.000}{1,25:#####0.00000000}");

            Console.WriteLine($"{"x",10}{"f(x)",25}");
            foreach (var s in strTab)
            {
                Console.WriteLine(s);
            }

            Lib.SaveTableToFile("out.txt", strTab, $"{"x",10}{"f(x)",25}");
        }
    }
}