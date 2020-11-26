using System;
using System.IO;
using Gtk;
using Task02Lib;
using UI = Gtk.Builder.ObjectAttribute;

namespace Task02GUI
{
    internal class MainWindow : Window
    {
        [UI] private Button _browsePath;
        [UI] private Button _calculateButton;
        [UI] private TextView _outputArea;
        [UI] private Label _pathLabel;
        [UI] private SpinButton _valueAInput;
        [UI] private SpinButton _valueBInput;
        [UI] private SpinButton _valueNInput;


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
            var a = _valueAInput.Value;
            var b = _valueBInput.Value;

            if (a > b || Math.Abs(a - b) < 1e-6)
            {
                _outputArea.Buffer.Text = "Value A can't be greater or equal than value B!";
                return;
            }

            var n = _valueNInput.ValueAsInt;

            var values = Lib.FunctionValues(
                x => Math.Sin(x) * Math.Pow(Math.Cos(x), 2) * (3 / Math.Exp(x)),
                a, b, (b - a) / n
            );
            var strValues = Lib.FunctionValuesToString(values,
                "{0,10:######0.000}{1,25:#####0.00000000}");
            _outputArea.Buffer.Text = string.Join("\n", strValues);

            if (File.Exists(_pathLabel.Text)) Lib.SaveTableToFile(_pathLabel.Text, strValues, $"{"x",10}{"f(x)",25}");
        }

        private void BrowseAction(object sender, EventArgs a)
        {
            var fcd = new FileChooserDialog("Choose file", this, FileChooserAction.Save);
            fcd.AddButton(Stock.Cancel, ResponseType.Cancel);
            fcd.AddButton(Stock.Open, ResponseType.Ok);
            fcd.DefaultResponse = ResponseType.Ok;
            fcd.SelectMultiple = false;

            var response = (ResponseType) fcd.Run();
            if (response == ResponseType.Ok)
            {
                _pathLabel.Text = fcd.Filename;
                if (!File.Exists(_pathLabel.Text))
                    File.Create(_pathLabel.Text);
            }

            fcd.Dispose();
        }
    }
}