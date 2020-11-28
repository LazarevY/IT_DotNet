using System;
using TechnicsLib;
using Reflections = ReflectionsUtils.Reflections;

namespace ConsoleTest
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Reflections r = new Reflections(
                "/home/lazarev/RiderProjects/IT_Tasks/IT_DotNet/Task06Sln/TechnicsLib/bin/Debug/TechnicsLib.dll");
            foreach (var type in r.AllImplementsOf(typeof(ITechnics)))
            {
                Console.WriteLine(type.ToString());
            }
        }
    }
}