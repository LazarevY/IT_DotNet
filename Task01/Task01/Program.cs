using System;
using System.Collections;
using System.Linq;

namespace Task01
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Matrix m = new Matrix(4, 4);

            m.SetRow(0, new []{1,1,10,1});
            m.SetRow(1, new []{1,2,3,4});
            m.SetRow(2, new []{4,3,2,1});
            m.SetRow(3, new []{1,1,5,1});


            MatrixProcess.SortByNonDecreasingSum(ref m);
            
            Console.WriteLine(m.ToString());

        }
    }
}