using System;
using System.Collections;
using System.Linq;

namespace Task01
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Matrix m = new Matrix(2, 2);

            m.Set(6, 0, 0);
            m.Set(2, 0, 1);
            m.Set(3, 1, 0);
            m.Set(4, 1, 1);


            MatrixProcess.SortBySum(ref m);
            
            Console.WriteLine(m.ToString());

        }
    }
}