using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Gtk;
using ModelDrawing;
using ModelsObjectsLib;
using ReflectionsUtils;
using UI = Gtk.Builder.ObjectAttribute;
using Model = ModelRun.ModelRun;

namespace Task08GUI
{
    internal class MainWindow : Window
    {
        [UI] private Button _activateButton;
        [UI] private SpinButton _brokeSpin;
        [UI] private SpinButton _capacitySpin;

        private int _currentIndex = 0;
        [UI] private Label _currentIndexLabel;
        [UI] private DrawingArea _drawArea;

        [UI] private SpinButton _genSpin;

        private readonly List<Model> _modelRuns = new List<Model>();
        [UI] private Button _nextModelButton;
        [UI] private Button _prevModelButtton;

        private readonly DrawManager DrawManager = new DrawManager(new Reflections(
            "/home/lazarev/RiderProjects/IT_Tasks/IT_DotNet/Task08Sln/ModelDrawing/bin/Debug/ModelDrawing.dll"));

        private ModelManager ModelManager;

        public MainWindow() : this(new Builder("MainWindow.glade"))
        {
        }

        private MainWindow(Builder builder) : base(builder.GetObject("MainWindow").Handle)
        {
            builder.Autoconnect(this);

            DeleteEvent += Window_DeleteEvent;

            InitModels();
            UpdateCurrentModel();

            _drawArea.Drawn += Draw;
            _drawArea.ShowAll();
        }

        private void ChangeGen(object? sender, EventArgs eventArgs)
        {
            _modelRuns[_currentIndex].FarmObject.Farm.GenChance = _genSpin.ValueAsInt;
        }

        private void ChangeBroke(object? sender, EventArgs eventArgs)
        {
            _modelRuns[_currentIndex].FarmObject.Farm.BrokeChance = _brokeSpin.ValueAsInt;
        }

        private void ChangeCapacity(object? sender, EventArgs eventArgs)
        {
            _modelRuns[_currentIndex].FarmObject.Farm.MilkStorage.Capacity = _capacitySpin.ValueAsInt;
        }

        private void ActivateModel(object? sender, EventArgs eventArgs)
        {
            _modelRuns[_currentIndex].Run();
            UpdateCurrentModel();
        }

        private void PrevModel(object? sender, EventArgs eventArgs)
        {
            _currentIndex = (_modelRuns.Count + _currentIndex - 1) % _modelRuns.Count;
            UpdateCurrentModel();
        }

        private void NextModel(object? sender, EventArgs eventArgs)
        {
            _currentIndex = (_modelRuns.Count + _currentIndex + 1) % _modelRuns.Count;
            UpdateCurrentModel();
        }

        private void UpdateCurrentModel()
        {
            var model = _modelRuns[_currentIndex];
            _genSpin.Value = model.FarmObject.Farm.GenChance;
            _brokeSpin.Value = model.FarmObject.Farm.BrokeChance;
            _capacitySpin.Value = model.FarmObject.Farm.MilkStorage.Capacity;
            _capacitySpin.Sensitive = !model.IsActive;
            _activateButton.Sensitive = !model.IsActive;
            _currentIndexLabel.Text = $"{_currentIndex}";
        }

        private void Draw(object o, DrawnArgs args)
        {
            foreach (var kv in ModelManager.AllModelObjects.OrderByDescending(pair => pair.Value.ZCoord))
            {
                var modelDrawBase = DrawManager.ForModel(kv.Value.GetType());
                modelDrawBase.Draw(kv.Value, args.Cr);
            }

            args.Cr.GetTarget().Dispose();
            args.Cr.Dispose();
        }

        private void InitModels()
        {
            ModelManager = new ModelManager();


            _modelRuns.Add(new Model(
                5,
                60,
                95,
                ModelManager,
                new Vector(120, 100),
                new Vector(240, 100)));
            _modelRuns.Add(new Model(
                20,
                60,
                45,
                ModelManager,
                new Vector(480, 100),
                new Vector(600, 100)));

            _modelRuns.Add(new Model(
                20,
                60,
                45,
                ModelManager,
                new Vector(120, 500),
                new Vector(240, 500)));
            _modelRuns.Add(new Model(
                20,
                60,
                45,
                ModelManager,
                new Vector(480, 500),
                new Vector(600, 500)));

            var t = new Thread(() =>
            {
                while (true)
                {
                    ModelManager.Update();
                    _drawArea.QueueDraw();
                    Thread.Sleep(500);
                }
            });
            t.Start();
        }


        private void Window_DeleteEvent(object sender, DeleteEventArgs a)
        {
            Application.Quit();
        }
    }
}