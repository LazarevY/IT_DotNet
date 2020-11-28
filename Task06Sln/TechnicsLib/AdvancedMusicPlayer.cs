using System;

namespace TechnicsLib
{
    public class AdvancedMusicPlayer : Player
    {
        public AdvancedMusicPlayer(string serialNumber) : base(serialNumber)
        {
            CurrentMediaName = "Song 1";
        }

        public AdvancedMusicPlayer() : this("DEFAULT")
        {
        }

        public bool Repeat { get; set; }

        public int Volume { get; set; }

        public SimpleEqualizer EqualizerSettings { get; set; } = new SimpleEqualizer();

        public void SetEqualizer(int low, int mid, int high)
        {
            EqualizerSettings.LowFreqLevel = low;
            EqualizerSettings.MiddleFreqLevel = mid;
            EqualizerSettings.HighFreqLevel = high;
        }

        public override void TurnOn()
        {
            base.TurnOn();
            Console.WriteLine("Advanced music player ready to play");
        }

        public override void TurnOff()
        {
            base.TurnOff();
            Console.WriteLine("Advanced music player has been disabled");
        }

        public override void Play()
        {
            base.Play();
            Console.WriteLine("Advanced music player play you cool track now");
        }

        public void NextTrack()
        {
            Console.WriteLine("Advanced player set next track");
        }

        public void PrevTrack()
        {
            Console.WriteLine("Advanced player set prev track");
        }

        public override void Pause()
        {
            base.Pause();
            Console.WriteLine("Advanced music player paused");
        }

        public void SkipBack(uint seconds)
        {
            Console.WriteLine("Skipped back " + seconds + " seconds");
        }

        public void SkipForward(uint seconds)
        {
            Console.WriteLine("Skipped forward " + seconds + " seconds");
        }
    }
}