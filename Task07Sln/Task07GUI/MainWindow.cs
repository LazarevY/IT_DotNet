using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using GLib;
using Gtk;
using ReflectionsUtils;
using TechnicsLib;
using Application = Gtk.Application;
using UI = Gtk.Builder.ObjectAttribute;

namespace Task07GUI
{
    class MainWindow : Window
    {
        [UI] private Box _methodsContainer;
        [UI] private Entry _classLibPath;
        [UI] private Button _serachButton;
        [UI] private Button _browseButton;
        [UI] private ComboBox _classComboBox;
        private Reflections _reflections;

        private void ScanLib(object? sender, EventArgs eventArgs)
        {
            _reflections = new Reflections(_classLibPath.Text);
            var allImplementsOf = _reflections.AllImplementsOf(typeof(ITechnics));
            
            _classComboBox.Clear();
            ListStore model = new ListStore(typeof(string), typeof(Type));
            

            foreach (var type in allImplementsOf)
            {
                var iter = model.AppendValues(type.Name, type);
            }
            
            CellRendererText c = new CellRendererText();
            _classComboBox.PackStart(c, true);
            _classComboBox.AddAttribute(c, "text", 0);
            _classComboBox.Model = model;

        }

        private void LoadClass(object? sender, EventArgs eventArgs)
        {
            foreach (var widget in _methodsContainer.Children)
            {
                _methodsContainer.Remove(widget);
            }

            TreeIter active;
            _classComboBox.GetActiveIter(out active);

            Type t = (Type) _classComboBox.Model.GetValue(active, 1);

            foreach (var method in t.GetMethods())
            {
                _methodsContainer.PackStart(
                    new MethodInvokeWidget<Type>(t, method),
                    true, true, 1 );

            }
        }
        private void Browse(object? sender, EventArgs eventArgs)
        {
            var fcd = new FileChooserDialog("Choose file", this, FileChooserAction.Open);
            fcd.AddButton(Stock.Cancel, ResponseType.Cancel);
            fcd.AddButton(Stock.Open, ResponseType.Ok);
            fcd.DefaultResponse = ResponseType.Ok;
            fcd.SelectMultiple = false;

            var response = (ResponseType) fcd.Run();
            if (response == ResponseType.Ok)
            {
                _classLibPath.Text = fcd.Filename;
            }

            fcd.Dispose();
        }

        public MainWindow() : this(new Builder("MainWindow.glade"))
        {
        }

        private MainWindow(Builder builder) : base(builder.GetObject("MainWindow").Handle)
        {
            builder.Autoconnect(this);

            DeleteEvent += Window_DeleteEvent;
        }
        

        private void Window_DeleteEvent(object sender, DeleteEventArgs a)
        {
            Application.Quit();
        }
    }
}