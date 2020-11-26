using System;
using System.Diagnostics;
using System.IO;

namespace Task02Lib

{
    public class Lib
    {
        public static void SaveTableToFile(string fileName, string[] table, string header)
        {
            using (var sw = File.CreateText(fileName))
            {
                sw.WriteLine(header);
                foreach (var s in table) sw.WriteLine(s);
            }
        }

        public static string[] FunctionValuesToString(
            (double, double)[] table,
            string format)
        {
            var stringTable = new string[table.Length];

            for (var row = 0; row < table.Length; ++row)
                stringTable[row] = string.Format(format,
                    table[row].Item1,
                    table[row].Item2);

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

            var countValues = (int) ((xEnd - xStart) / xStep) + 1;

            var retTable = new (double, double)[countValues];


            for (var stepCounter = 0; stepCounter < countValues; ++stepCounter)
            {
                var x = xStart + stepCounter * xStep;
                retTable[stepCounter] = (x, function(x));
            }

            return retTable;
        }
    }
}