using System;

namespace TechnicsLib
{
    public class VideoPlayer : Player
    {
        public VideoPlayer()
        {
            CurrentMediaName = "Video " + new Random().Next();
        }

        public string ColorGamma => "256 RGB colors";
        public (int, int) Dimension => (480, 320);

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
    }
}