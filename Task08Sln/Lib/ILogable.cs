namespace Lib
{
    public interface ILogable
    {
        public delegate void MessageHandler(string msg);

        public event MessageHandler ThrowMessage;
    }
}