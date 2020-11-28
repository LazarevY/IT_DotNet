using System;
using System.Collections.Generic;

namespace TechnicsLib
{
    public class VideoPlayer : Player
    {
        public enum AudioCodecs
        {
            MP3,
            WMA,
            FLAC
        }

        public enum VideoCodecs
        {
            MKV,
            H_264,
            H_265,
            TS
        }

        public VideoPlayer(
            string serialNumber,
            int ramMB,
            HashSet<VideoCodecs> videoCodecs,
            HashSet<AudioCodecs> audioCodecs) : base(serialNumber)
        {
            CurrentMediaName = "Video " + new Random().Next();
            PlayerVideoCodecs = videoCodecs;
            PlayerAudioCodecs = audioCodecs;
            RamMB = ramMB;
        }

        public HashSet<VideoCodecs> PlayerVideoCodecs { get; }
        public HashSet<AudioCodecs> PlayerAudioCodecs { get; }

        public int RamMB { get; set; }

        public bool Repeat { get; set; }

        public override void TurnOn()
        {
            base.TurnOn();
            Console.WriteLine("Hi! I'm ready to play video!");
        }

        public override void TurnOff()
        {
            Console.WriteLine("Goodbye!");
            base.TurnOff();
        }

        public override void Play()
        {
            Console.WriteLine("Play video : " + CurrentMediaName);
        }

        public override void Pause()
        {
            Console.WriteLine("Video Player paused");
        }

        public void SkipBack(uint seconds)
        {
            Console.WriteLine("Skipped back " + seconds + " seconds");
        }

        public void SkipForward(uint seconds)
        {
            Console.WriteLine("Skipped forward " + seconds + " seconds");
        }

        public void NextVideo()
        {
            CurrentMediaName = "Video " + new Random().Next();
        }

        public void PreviousVideo()
        {
            CurrentMediaName = "Video " + new Random().Next();
        }

        public override string ToString()
        {
            CodecsToString c = new CodecsToString();
            return $"I am Video player.\nMy S\\N is {SerialNumber}.\n" +
                   $"Support video codecs {c.Process(PlayerVideoCodecs)}.\nSupport audio codecs: {c.Process(PlayerAudioCodecs)}\n" +
                   $"Ram: {RamMB} MB";
        }
    }
}