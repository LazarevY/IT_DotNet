using System;
using System.Threading;
using Lib;

namespace ConsoleTest
{
    internal class Program
    {
        public static void Main(string[] args)
        {
             Storage s = new Storage(30);
             MilkFarm farm = new MilkFarm{GenChance = 70, MilkStorage = s};
             farm.ThrowMessage += Console.WriteLine;
             HardMechanic mechanic = new HardMechanic();
             mechanic.ThrowMessage += Console.WriteLine;
             farm.EquipmentBroken += mechanic.Fix;
             Thread t = new Thread(() =>
             {
                 while (true)
                 {
                     farm.Update();
                     Thread.Sleep(100);
                 }
             });
             t.Start();
        }
    }
}