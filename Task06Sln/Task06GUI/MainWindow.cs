using System;
using System.Collections.Generic;
using System.Linq;
using Gtk;
using TechnicsLib;
using UI = Gtk.Builder.ObjectAttribute;

namespace Task06GUI
{
    internal class MainWindow : Window
    {
        private readonly List<CheckButton> _audioCodecsChoices = new List<CheckButton>();
        [UI] private Box _audioCodecsInContainer;
        private ITechnics _currentPlayer;
        [UI] private Label _enabledStatusLabel;
        [UI] private TextView _guiConsoleTextView;
        private int _index;
        [UI] private Label _infoLabel;
        [UI] private Button _nextPlayerButton;
        [UI] private Button _offButton;
        [UI] private Button _onButton;
        [UI] private Button _previousPlayerButton;
        [UI] private SpinButton _ramInput;
        [UI] private Entry _serailInput;

        private readonly List<ITechnics> _technicses;
        private readonly List<CheckButton> _vidCodecsChoices = new List<CheckButton>();
        [UI] private Box _videoCodecsInContainer;

        public MainWindow() : this(new Builder("MainWindow.glade"))
        {
        }

        private MainWindow(Builder builder) : base(builder.GetObject("MainWindow").Handle)
        {
            builder.Autoconnect(this);

            DeleteEvent += Window_DeleteEvent;

            _technicses = new List<ITechnics>();
            AddPlayers();
        }

        private void AddPlayers()
        {
            _technicses.Add(new VideoPlayer("SN16789", 4096,
                new HashSet<VideoPlayer.VideoCodecs>
                    {VideoPlayer.VideoCodecs.H_264, VideoPlayer.VideoCodecs.H_265, VideoPlayer.VideoCodecs.TS},
                new HashSet<VideoPlayer.AudioCodecs> {VideoPlayer.AudioCodecs.MP3, VideoPlayer.AudioCodecs.WMA}));
            _technicses.Add(new VideoPlayer("SN162345", 4096,
                new HashSet<VideoPlayer.VideoCodecs>
                {
                    VideoPlayer.VideoCodecs.H_264, VideoPlayer.VideoCodecs.H_265, VideoPlayer.VideoCodecs.TS,
                    VideoPlayer.VideoCodecs.MKV
                },
                new HashSet<VideoPlayer.AudioCodecs>
                    {VideoPlayer.AudioCodecs.MP3, VideoPlayer.AudioCodecs.WMA, VideoPlayer.AudioCodecs.FLAC}));
            SetCurrentPlayer(_technicses[0]);
            InitFields();
        }

        private void SetCurrentPlayer(ITechnics player)
        {
            _currentPlayer = player;
            _enabledStatusLabel.Text = player.Enabled ? "Enabled" : "Disabled";
            _infoLabel.Text = player.ToString();
        }

        private void ToPreviousPlayer(object? sender, EventArgs eventArgs)
        {
            --_index;
            UpdateIndexPolicy();
            if (_technicses.Count == 0)
                return;
            SetCurrentPlayer(_technicses[_index]);
        }

        private void ToNextPlayer(object? sender, EventArgs eventArgs)
        {
            ++_index;
            UpdateIndexPolicy();
            if (_technicses.Count == 0)
                return;
            SetCurrentPlayer(_technicses[_index]);
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

        private void InitFields()
        {
            foreach (var widget in _videoCodecsInContainer.Children) _videoCodecsInContainer.Remove(widget);

            foreach (var widget in _audioCodecsInContainer.Children) _audioCodecsInContainer.Remove(widget);

            _videoCodecsInContainer.PackStart(new Label("Video codecs"), false, true, 3);
            _audioCodecsInContainer.PackStart(new Label("Audio codecs"), false, true, 3);

            foreach (var value in Enum.GetNames(typeof(VideoPlayer.VideoCodecs)))
            {
                var b = new CheckButton(value);
                _vidCodecsChoices.Add(b);
                _videoCodecsInContainer.PackStart(b, false, true, 3);
            }

            foreach (var value in Enum.GetNames(typeof(VideoPlayer.AudioCodecs)))
            {
                var b = new CheckButton(value);
                _audioCodecsChoices.Add(b);
                _audioCodecsInContainer.PackStart(b, false, true, 3);
            }

            _videoCodecsInContainer.ShowAll();
            _audioCodecsInContainer.ShowAll();
        }

        private void AddPlayer(object? sender, EventArgs eventArgs)
        {
            var vidSet = new HashSet<VideoPlayer.VideoCodecs>(_vidCodecsChoices
                .Where(b => b.Active)
                .Select(b => Enum.Parse<VideoPlayer.VideoCodecs>(b.Label)));
            var audSet = new HashSet<VideoPlayer.AudioCodecs>(_audioCodecsChoices
                .Where(b => b.Active)
                .Select(b => Enum.Parse<VideoPlayer.AudioCodecs>(b.Label)));

            if (vidSet.Count == 0)
                vidSet.Add(VideoPlayer.VideoCodecs.H_264);
            if (audSet.Count == 0)
                audSet.Add(VideoPlayer.AudioCodecs.MP3);

            var sn = _serailInput.Text.Equals("") ? "DEFAULT" : _serailInput.Text;

            _technicses.Add(new VideoPlayer(
                sn,
                _ramInput.ValueAsInt,
                vidSet,
                audSet
            ));
            SetCurrentPlayer(_technicses[_index]);
        }

        private void RemovePlayer(object? sender, EventArgs eventArgs)
        {
            if (_technicses.Count == 0) return;

            _technicses.RemoveAt(_index);
            if (_technicses.Count == 0)
            {
                _enabledStatusLabel.Text = "";
                _infoLabel.Text = "";
                UpdateIndexPolicy();
                return;
            }
            UpdateIndexPolicy();
            SetCurrentPlayer(_technicses[_index]);
        }

        private void UpdateIndexPolicy()
        {
            if (_technicses.Count == 0)
            {
                _index = 0;
                return;
            }

            _index = (_index + _technicses.Count) % _technicses.Count;
        }


        private void Window_DeleteEvent(object sender, DeleteEventArgs a)
        {
            Application.Quit();
        }
    }
}