using System;

namespace TechnicsLib
{
    public class VideoPlayer : Player
    {
        public VideoPlayer(string serialNumber, string colorGamma, (int, int) dimension) : base(serialNumber)
        {
            CurrentMediaName = "Video " + new Random().Next();
            ColorGamma = colorGamma;
            Dimension = dimension;
        }

        public string ColorGamma { get; private set; }

        public (int, int) Dimension { get; private set; }

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
            return $"I am Video player.\nMy S\\N is {SerialNumber}.\n" +
                   $"Color gamma: {ColorGamma}.\nResolution: {Dimension}";
        }
    }
}