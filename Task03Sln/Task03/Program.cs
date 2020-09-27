using System;

namespace Task03
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Number n = new SimpleFraction(3,2);
            Console.WriteLine(n.ToString());
            Console.WriteLine(n.NormalRepresent());
        }
    }
}