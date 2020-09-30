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
                throw new DivideByZeroException("0 cannot be used as fraction denominator!");
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
                _denominator * casted._denominator).Reduced();
        }

        public override Number Sub(Number other)
        {
            SimpleFraction casted = (SimpleFraction) other;
            return new SimpleFraction(
                _nominator * casted._denominator - casted.Nominator * _denominator,
                _denominator * casted._denominator).Reduced();
        }

        public override Number Multiply(Number other)
        {
            SimpleFraction casted = (SimpleFraction) other;
            return new SimpleFraction(
                _nominator * casted._nominator,
                _denominator * casted._denominator);
        }

        public override Number Divide(Number other)
        {
            try
            {
                SimpleFraction casted = (SimpleFraction) other;
                int sign = (_nominator * casted._nominator);
                
                if (sign == 0)
                    sign = 1;
                else
                    sign /= Math.Abs(_nominator * casted._nominator);
                return new SimpleFraction(
                    Math.Abs(_nominator * casted._denominator) * sign,
                    Math.Abs(_denominator * casted._nominator));
            }
            catch (DivideByZeroException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
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
            SimpleFraction s = Reduced();
            return $"{s.Nominator}/{s.Denominator}";
        }

        public override string NormalRepresent()
        {
            if (_nominator == 0)
                return "0";
            SimpleFraction s = Reduced();
            int whole = s.Nominator / s.Denominator;
            int wholeUnsigned = Math.Abs(whole);
            return wholeUnsigned > 0 ? 
                (s.Nominator  % s.Denominator != 0? $"{whole}({Math.Abs(s.Nominator) % s.Denominator}/{s.Denominator})" : 
                    $"{whole}") : ToString();
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