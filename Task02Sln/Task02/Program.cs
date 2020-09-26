using System;
using System.Diagnostics;

namespace Task02
{

    internal class Program
    {

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
            Func<double, double> fX = x => Math.Sin(x) * Math.Pow(Math.Cos(x), 2) * Math.Pow(Math.Exp(x), 3);
            double xStart = 0, xEnd = 2;
            int N = 5;
            var table = FunctionValues(fX, xStart, xEnd, (xEnd - xStart) / N);

            var strTab = FunctionValuesToString(table,
                "{0,10:######0.000}{1,25:#####0.00000000}");

            foreach (var s in strTab)
            {
                Console.WriteLine(s);
            }

        }
    }
}