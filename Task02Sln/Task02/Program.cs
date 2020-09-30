using System;
using System.Diagnostics;
using System.IO;

namespace Task02
{

    internal class Program
    {

        public static void SaveTableToFile(string fileName, string[] table, string header)
        {
            using (StreamWriter sw = File.CreateText(fileName))
            {
                sw.WriteLine(header);
                foreach (var s in table)
                {
                    sw.WriteLine(s);
                }
            }
        }

        public static string[] FunctionValuesToString(
            (double, double)[] table,
            string format)
        {
            var stringTable = new string[table.Length];

            for (int row = 0; row < table.Length; ++row)
            {
                stringTable[row] = String.Format(format,
                    table[row].Item1,
                    table[row].Item2);
            }

            return stringTable;
        }

        public static (double, double)[] FunctionValues(
            Func<double, double> function,
            double xStart,
            double xEnd,
            double xStep)
        {
            Debug.Assert(xStart < xEnd);
            Debug.Assert(xStep > 0);

            int countValues = (int) ((xEnd - xStart) / xStep) + 1;
            
            var retTable = new (double, double)[countValues];

         
            for (int stepCounter = 0; stepCounter < countValues; ++stepCounter)
            {
                double x = xStart + stepCounter * xStep;
                retTable[stepCounter] = (x, function(x));
            }

            return retTable;

        }

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

            var table = FunctionValues(fX, xStart, xEnd, (xEnd - xStart) / n);

            var strTab = FunctionValuesToString(table,
                "{0,10:######0.000}{1,25:#####0.00000000}");

            Console.WriteLine($"{"x",10}{"f(x)",25}");
            foreach (var s in strTab)
            {
                Console.WriteLine(s);
            }
            
            SaveTableToFile("out.txt", strTab, $"{"x",10}{"f(x)",25}");

        }
    }
}