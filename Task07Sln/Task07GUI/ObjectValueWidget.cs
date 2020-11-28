using Gtk;

namespace Task07GUI
{
    public class ObjectValueWidget : Label, IGetValueWidget
    {
        public ObjectValueWidget() : base("This is mock widget for type 'object'")
        {
            
        }
        public object GetCurrentValue()
        {
            return new object();
        }
    }
}