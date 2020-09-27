using System;

namespace Task03
{
    public class SimpleFraction : Number
    {
        private int _nominator;
        private int _denominator;

        public SimpleFraction(int nominator, int denominator)
        {
            if (denominator == 0)
                throw new ArgumentException("0 cannot be used as fraction denominator!");
            _nominator = nominator;
            _denominator = denominator;
        }

        public int Nominator
        {
            get => _nominator;
            set => _nominator = value;
        }

        public int Denominator
        {
            get => _denominator;
            set => _denominator = value;
        }

        public override Number Add(Number other)
        {
            SimpleFraction casted = (SimpleFraction) other;
            return new SimpleFraction(
                _nominator * casted._denominator + casted.Nominator * _denominator,
                this._denominator * casted._denominator).Reduced();
        }

        public override Number Sub(Number other)
        {
            SimpleFraction casted = (SimpleFraction) other;
            return new SimpleFraction(
                _nominator * casted._denominator - casted.Nominator * _denominator,
                this._denominator * casted._denominator).Reduced();
        }

        public override Number Multiply(Number other)
        {
            SimpleFraction casted = (SimpleFraction) other;
            return new SimpleFraction(
                this._nominator * casted._nominator,
                this._denominator * casted._denominator);
        }

        public override Number Divide(Number other)
        {
            SimpleFraction casted = (SimpleFraction) other;
            return new SimpleFraction(
                this._nominator * casted._denominator,
                this._denominator * casted._nominator);
        }

        public void Reduce()
        {
            int gdc = Gdc(Math.Abs(_nominator), Math.Abs(_denominator));
            _nominator /= gdc;
            _denominator /= gdc;
        }

        public SimpleFraction Reduced()
        {
            SimpleFraction reduced = new SimpleFraction(_nominator, _denominator);
            reduced.Reduce();
            return reduced;
        }

        public override string ToString()
        {
            return $"{_nominator}/{_denominator}";
        }

        public override string NormalRepresent()
        {
            int whole = _nominator / _denominator;
            return whole > 0 ? $"{whole}({_nominator % _denominator}/{_denominator})" : ToString();
        }

        private static int Gdc(int a, int b)
        {
            while (true)
            {
                a = a % b;
                if (a == 0)
                    return b;
                b = b % a;
                if (b == 0)
                    return a;
            }
        }
    }
}