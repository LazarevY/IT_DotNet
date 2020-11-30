using Gtk;

namespace Task07GUI
{
    public class BooleanGetWidget: CheckButton, IGetValueWidget
    {
        public object GetCurrentValue()
        {
            return Active;
        }
    }
}