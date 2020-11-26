using System;

namespace Task03
{
    public class SimpleFraction : Number
    {
        public SimpleFraction(int nominator, int denominator)
        {
            if (denominator == 0)
                throw new DivideByZeroException("0 cannot be used as fraction denominator!");
            Nominator = nominator;
            Denominator = denominator;
        }

        public int Nominator { get; set; }

        public int Denominator { get; set; }


        public override Number Add(Number other)
        {
            var casted = (SimpleFraction) other;
            return new SimpleFraction(
                Nominator * casted.Denominator + casted.Nominator * Denominator,
                Denominator * casted.Denominator).Reduced();
        }

        public override Number Sub(Number other)
        {
            var casted = (SimpleFraction) other;
            return new SimpleFraction(
                Nominator * casted.Denominator - casted.Nominator * Denominator,
                Denominator * casted.Denominator).Reduced();
        }

        public override Number Multiply(Number other)
        {
            var casted = (SimpleFraction) other;
            return new SimpleFraction(
                Nominator * casted.Nominator,
                Denominator * casted.Denominator);
        }

        public override Number Divide(Number other)
        {
            try
            {
                var casted = (SimpleFraction) other;
                var sign = Nominator * casted.Nominator;

                if (sign == 0)
                    sign = 1;
                else
                    sign /= Math.Abs(Nominator * casted.Nominator);
                return new SimpleFraction(
                    Math.Abs(Nominator * casted.Denominator) * sign,
                    Math.Abs(Denominator * casted.Nominator));
            }
            catch (DivideByZeroException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public void Reduce()
        {
            var gdc = Utils.Gdc(Math.Abs(Nominator), Math.Abs(Denominator));
            Nominator /= gdc;
            Denominator /= gdc;
        }

        public SimpleFraction Reduced()
        {
            var reduced = new SimpleFraction(Nominator, Denominator);
            reduced.Reduce();
            return reduced;
        }

        public override string ToString()
        {
            var s = Reduced();
            return $"{s.Nominator}/{s.Denominator}";
        }

        public override string NormalRepresent()
        {
            if (Nominator == 0)
                return "0";
            var s = Reduced();
            var whole = s.Nominator / s.Denominator;
            var wholeUnsigned = Math.Abs(whole);
            return wholeUnsigned > 0
                ? s.Nominator % s.Denominator != 0
                    ? $"{whole}({Math.Abs(s.Nominator) % s.Denominator}/{s.Denominator})"
                    : $"{whole}"
                : ToString();
        }
    }
}