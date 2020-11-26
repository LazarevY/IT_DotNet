namespace TechnicsLib
{
    public interface ITechnics
    {
        bool Enabled { get;}

        void TurnOn();
        void TurnOff();
    }
}