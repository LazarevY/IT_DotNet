using System;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;
using Task03;

namespace Task03GUI
{
    class MainWindow : Window
    {
        [UI] private SpinButton _firstFractionNominatorEdit;
        [UI] private SpinButton _firstFractionDenominatorEdit;
        [UI] private SpinButton _secondFractionNominatorEdit;
        [UI] private SpinButton _secondFractionDenominatorEdit;

        [UI] private Label _firstFractionView;
        [UI] private Label _secondFractionView;

        [UI] private Button _addButton;
        [UI] private Button _subButton;
        [UI] private Button _multiplyButton;
        [UI] private Button _divideButton;

        [UI] private Label _resView;
        [UI] private Label _resNormalView;

        private SimpleFraction _firstFraction;
        private SimpleFraction _secondFraction;

        public MainWindow() : this(new Builder("MainWindow.glade"))
        {
        }

        private MainWindow(Builder builder) : base(builder.GetObject("MainWindow").Handle)
        {
            builder.Autoconnect(this);
            
            _firstFraction = new SimpleFraction(1,1);
            _secondFraction = new SimpleFraction(1,1);

            DeleteEvent += Window_DeleteEvent;
            _firstFractionNominatorEdit.ValueChanged += FirstFractionNominatorEditValueChanged;
            _firstFractionDenominatorEdit.ValueChanged += FirstFractionDenominatorEditValueChanged;
            _secondFractionNominatorEdit.ValueChanged += SecondFractionNominatorEditValueChanged;
            _secondFractionDenominatorEdit.ValueChanged += SecondFractionDenominatorEditValueChanged;

            _addButton.Clicked += AddAction;
            _subButton.Clicked += SubAction;
            _multiplyButton.Clicked += MultiplyAction;
            _divideButton.Clicked += DivideAction;
            _firstFractionView.Text = _firstFraction.NormalRepresent();
            _secondFractionView.Text = _secondFraction.NormalRepresent();
        }

        private void Window_DeleteEvent(object sender, DeleteEventArgs a)
        {
            Application.Quit();
        }

        private void FirstFractionNominatorEditValueChanged(object? sender, EventArgs eventArgs)
        {
            _firstFraction.Nominator = _firstFractionNominatorEdit.ValueAsInt;
            _firstFractionView.Text = _firstFraction.NormalRepresent();
        }

        private void FirstFractionDenominatorEditValueChanged(object? sender, EventArgs eventArgs)
        {
            _firstFraction.Denominator = _firstFractionDenominatorEdit.ValueAsInt;
            _firstFractionView.Text = _firstFraction.NormalRepresent();
        }

        private void SecondFractionNominatorEditValueChanged(object? sender, EventArgs eventArgs)
        {
            _secondFraction.Nominator = _secondFractionNominatorEdit.ValueAsInt;
            _secondFractionView.Text = _secondFraction.NormalRepresent();
        }

        private void SecondFractionDenominatorEditValueChanged(object? sender, EventArgs eventArgs)
        {
            _secondFraction.Denominator = _secondFractionDenominatorEdit.ValueAsInt;
            _secondFractionView.Text = _secondFraction.NormalRepresent();
        }

        private void AddAction(object? sender, EventArgs eventArgs)
        {
            SimpleFraction res = (SimpleFraction)_firstFraction.Add(_secondFraction);
            _resView.Text = res.ToString();
            _resNormalView.Text = res.NormalRepresent();
        }

        private void SubAction(object? sender, EventArgs eventArgs)
        {
            SimpleFraction res = (SimpleFraction)_firstFraction.Sub(_secondFraction);
            _resView.Text = res.ToString();
            _resNormalView.Text = res.NormalRepresent();
        }        
        private void MultiplyAction(object? sender, EventArgs eventArgs)
        {
            SimpleFraction res = (SimpleFraction)_firstFraction.Multiply(_secondFraction);
            _resView.Text = res.ToString();
            _resNormalView.Text = res.NormalRepresent();
        }

        private void DivideAction(object? sender, EventArgs eventArgs)
        {
            try
            {
                SimpleFraction res = (SimpleFraction) _firstFraction.Divide(_secondFraction);
                _resView.Text = res.ToString();
                _resNormalView.Text = res.NormalRepresent();
            }
            catch (DivideByZeroException e)
            {
                _resView.Text = "Divide by zero!";
                _resNormalView.Text = "Divide by zero!";
            }
        }
    }
}