using System;
using System.Threading.Tasks;
using TechnicsLib;

namespace ConsoleTest
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            VideoPlayer player = new VideoPlayer();
            player.TurnOn();
            player.Play();
            player.NextVideo();
            player.Play();
            player.TurnOff();
        }
    }
}