using System.Threading;

namespace Lib
{
    public class HardMechanic : IMechanic, ILogable
    {
        public event ILogable.MessageHandler ThrowMessage;

        public void Fix(Equipment equipment)
        {
            equipment.Strength = 100;
            ThrowMessage?.Invoke("Я каменщик работаю 3 дня Без зарплаты");
        }
    }
}