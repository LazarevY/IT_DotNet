using System;
using Gtk;
using TechnicsLib;
using UI = Gtk.Builder.ObjectAttribute;

namespace Task07GUI
{
    class MainWindow : Window
    {
        [UI] private Box _methodsContainer;
        [UI] private Entry _classLibPath;
        [UI] private Button _serachButton;
        [UI] private ComboBox _classComboBox;
        private void ScanLib(object? sender, EventArgs eventArgs){}
        private void LoadClass(object? sender, EventArgs eventArgs){}

        public MainWindow() : this(new Builder("MainWindow.glade"))
        {
        }

        private MainWindow(Builder builder) : base(builder.GetObject("MainWindow").Handle)
        {
            builder.Autoconnect(this);

            DeleteEvent += Window_DeleteEvent;
            MethodInvokeWidget<VideoPlayer>
            methodInvokeWidget = new MethodInvokeWidget<VideoPlayer>(typeof(AdvancedMusicPlayer), typeof(AdvancedMusicPlayer).GetMethod("SetEqualizer"));
            _methodsContainer.PackStart(methodInvokeWidget, true, true, 0);
            _methodsContainer.ShowAll();
        }

        private void Window_DeleteEvent(object sender, DeleteEventArgs a)
        {
            Application.Quit();
        }
    }
}