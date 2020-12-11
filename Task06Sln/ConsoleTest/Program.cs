using System;
using System.Collections.Generic;
using TechnicsLib;

namespace ConsoleTest
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var player = new VideoPlayer(
                "SN394347374",
                1024,
                new HashSet<VideoPlayer.VideoCodecs> {VideoPlayer.VideoCodecs.H_264, VideoPlayer.VideoCodecs.H_265},
                new HashSet<VideoPlayer.AudioCodecs> {VideoPlayer.AudioCodecs.MP3, VideoPlayer.AudioCodecs.WMA});
            player.TurnOn();
            player.Play();
            player.NextVideo();
            player.Play();
            player.TurnOff();
            Console.WriteLine(player.ToString());
        }
    }
}