using System;
using TechnicsLib;

namespace ConsoleTest
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var player = new VideoPlayer("SN394347374", "22", (100, 100));
            player.TurnOn();
            player.Play();
            player.NextVideo();
            player.Play();
            player.TurnOff();
            Console.WriteLine(player.ToString());
        }
    }
}