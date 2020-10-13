using System;
using System.IO;
using System.Linq;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;
using Task02Lib;
namespace Task02GUI
{
    class MainWindow : Window
    {
        [UI] private SpinButton _valueAInput;
        [UI] private SpinButton _valueBInput;
        [UI] private SpinButton _valueNInput;
        [UI] private Button _browsePath;
        [UI] private Label _pathLabel;
        [UI] private Button _calculateButton;
        [UI] private TextView _outputArea;


        public MainWindow() : this(new Builder("MainWindow.glade"))
        {
        }

        private MainWindow(Builder builder) : base(builder.GetObject("MainWindow").Handle)
        {
            builder.Autoconnect(this);

            DeleteEvent += Window_DeleteEvent;
            //_browsePath.Clicked += BrowseAction;
            _calculateButton.Clicked += CalculateAction;
        }

        private void Window_DeleteEvent(object sender, DeleteEventArgs a)
        {
            Application.Quit();
        }

        private void CalculateAction(object sender, EventArgs args)
        {
            double a = _valueAInput.Value;
            double b = _valueBInput.Value;

            if (a > b || Math.Abs(a-b) < 1e-6)
            {
                _outputArea.Buffer.Text = "Value A can't be greater or equal than value B!";
                return;
            }
            
            int n = _valueNInput.ValueAsInt;

            var values = Lib.FunctionValues(
                (x => Math.Sin(x) * Math.Pow(Math.Cos(x), 2) * (3 / Math.Exp(x))), 
                a, b, (b - a) / n
                );
            var strValues = Lib.FunctionValuesToString(values,
                "{0,10:######0.000}{1,25:#####0.00000000}");
            _outputArea.Buffer.Text = String.Join("\n", strValues);
            
            if (File.Exists(_pathLabel.Text))
            {
                Lib.SaveTableToFile(_pathLabel.Text, strValues, $"{"x",10}{"f(x)",25}");
            }

        }        
        private void BrowseAction(object sender, EventArgs a)
        {
            var fcd= new FileChooserDialog("Choose file", this, FileChooserAction.Save);
            fcd.AddButton (Gtk.Stock.Cancel, Gtk.ResponseType.Cancel);
            fcd.AddButton (Gtk.Stock.Open, Gtk.ResponseType.Ok);
            fcd.DefaultResponse = Gtk.ResponseType.Ok;
            fcd.SelectMultiple = false;

            Gtk.ResponseType response = (Gtk.ResponseType) fcd.Run ();
            if (response == Gtk.ResponseType.Ok)
            {
                _pathLabel.Text = fcd.Filename;
                if (!File.Exists(_pathLabel.Text))
                    File.Create(_pathLabel.Text);
            }

            fcd.Dispose();
        }
    }
}