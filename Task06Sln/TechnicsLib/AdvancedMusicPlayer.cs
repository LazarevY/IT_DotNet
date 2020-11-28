namespace TechnicsLib
{
    public class AdvancedMusicPlayer : Player
    {
        public AdvancedMusicPlayer(string serialNumber) : base(serialNumber)
        {
            CurrentMediaName = "Song 1";
        }

        public override void TurnOn()
        {
            base.TurnOn();
        }

        public override void TurnOff()
        {
            base.TurnOff();
        }

        public override void Play()
        {
            base.Play();
        }

        public override void Pause()
        {
            base.Pause();
        }
    }
}