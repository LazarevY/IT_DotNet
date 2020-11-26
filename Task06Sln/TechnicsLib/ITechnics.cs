namespace TechnicsLib
{
    public interface ITechnics
    {
        bool Enabled { get; }

        string SerialNumber { get; }

        void TurnOn();
        void TurnOff();
    }
}