using System;
using Gtk;

namespace Task07GUI
{
    public class IntegerValueWidget: SpinButton, IGetValueWidget
    {
        public IntegerValueWidget() : base(double.MinValue, Double.MaxValue, 1)
        {
            Value = 0;
        }

        public object GetCurrentValue()
        {
            return ValueAsInt;
        }
    }
}