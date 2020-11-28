using Gtk;

namespace Task07GUI
{
    public class UnsignedValueWidget: SpinButton, IGetValueWidget
    {
        public UnsignedValueWidget() : base(0, double.MaxValue, 1)
        {
        }

        public object GetCurrentValue()
        {
            return (uint) ValueAsInt;
        }
    }
}