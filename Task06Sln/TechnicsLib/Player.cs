namespace TechnicsLib
{
    public abstract class Player : ITechnics
    {
        public Player(string serialNumber)
        {
            SerialNumber = serialNumber;
        }

        public string CurrentMediaName { get; protected set; }
        public bool Enabled { get; protected set; }
        public string SerialNumber { get; protected set; }

        public virtual void TurnOn()
        {
            Enabled = true;
        }

        public virtual void TurnOff()
        {
            Enabled = false;
        }

        public virtual void Play()
        {
        }

        public virtual void Pause()
        {
        }
    }
}