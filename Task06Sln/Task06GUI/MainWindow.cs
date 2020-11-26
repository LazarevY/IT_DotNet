using System;
using System.Collections.Generic;
using Gtk;
using TechnicsLib;
using UI = Gtk.Builder.ObjectAttribute;

namespace Task06GUI
{
    internal class MainWindow : Window
    {
        [UI] private Button _previousPlayerButton;
        [UI] private Button _nextPlayerButton;
        [UI] private Label _enabledStatusLabel;
        [UI] private Button _onButton;
        [UI] private Button _offButton;
        [UI] private TextView _guiConsoleTextView;
        [UI] private Label _infoLabel;

        private List<ITechnics> _technicses;
        private ITechnics _currentPlayer;
        private int _index = 0;

        public MainWindow() : this(new Builder("MainWindow.glade"))
        {
        }

        private MainWindow(Builder builder) : base(builder.GetObject("MainWindow").Handle)
        {
            builder.Autoconnect(this);

            DeleteEvent += Window_DeleteEvent;

            _technicses =  new List<ITechnics>();
            AddPlayers();
        }

        private void AddPlayers()
        {
            _technicses.Add(new VideoPlayer("SN1625", "256 RGB colors", (480, 320)));
            _technicses.Add(new VideoPlayer("SN1623", "65635 RGB colors", (720, 480)));
            _technicses.Add(new VideoPlayer("SN1645", "65635 RGB colors", (1080, 6720)));
            _technicses.Add(new VideoPlayer("SN16789", "16M RGB colors", (1080, 1024)));
            _technicses.Add(new VideoPlayer("SN162345", "16M RGB colors", (1920, 1080)));
            setCurrentPlayer(_technicses[0]);
        }

        private void setCurrentPlayer(ITechnics player)
        {
            _currentPlayer = player;
            _enabledStatusLabel.Text = player.Enabled ? "Enabled" : "Disabled";
            _infoLabel.Text = player.ToString();
        }

        private void ToPreviousPlayer(object? sender, EventArgs eventArgs)
        {
            if (--_index < 0)
                _index = _technicses.Count - 1;
            setCurrentPlayer(_technicses[_index]);
        }

        private void ToNextPlayer(object? sender, EventArgs eventArgs)
        {
            if (++_index >= _technicses.Count)
                _index = 0;
            setCurrentPlayer(_technicses[_index]);
        }

        private void TurnOn(object? sender, EventArgs eventArgs)
        {
            _currentPlayer.TurnOn();
            _enabledStatusLabel.Text = "Enabled";
        }

        private void TurnOff(object? sender, EventArgs eventArgs)
        {
            _currentPlayer.TurnOff();
            _enabledStatusLabel.Text = "Disabled";
        }

        private void Window_DeleteEvent(object sender, DeleteEventArgs a)
        {
            Application.Quit();
        }
        
    }
}