namespace Task03
{
    public abstract class Number
    {
        public abstract Number Add(Number other);
        public abstract Number Sub(Number other);
        public abstract Number Multiply(Number other);
        public abstract Number Divide(Number other);
        public abstract string NormalRepresent();
    }
}