using System;
using System.Diagnostics;
using System.IO;

namespace Task02Lib

{
    public class Lib
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
    }
}