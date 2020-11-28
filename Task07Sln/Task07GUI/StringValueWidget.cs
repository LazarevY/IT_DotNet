using Gtk;

namespace Task07GUI
{
    public class StringValueWidget : Entry, IGetValueWidget
    {
        public object GetCurrentValue()
        {
            return Text;
        }
    }
}