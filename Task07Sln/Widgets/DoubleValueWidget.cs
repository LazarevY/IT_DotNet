using Gtk;

namespace Task07GUI
{
    public class DoubleValueWidget : SpinButton, IGetValueWidget
    {
        public DoubleValueWidget() : base(double.MinValue, double.MaxValue, 1)
        {
        }

        public object GetCurrentValue()
        {
            return Value;
        }
    }
}