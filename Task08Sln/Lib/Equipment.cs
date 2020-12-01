namespace Lib
{
    public abstract class Equipment
    {

        private int _strength = 100;
        public int Strength
        {
            get => _strength;
            set
            {
                _strength = value;
                ValidateStrength();
            }
        }

        public bool Enabled => Strength > 0;

        private void ValidateStrength()
        {
            if (_strength < 0)
                _strength = 0;
            else if (_strength > 100)
                _strength = 100;
        }
    }
}