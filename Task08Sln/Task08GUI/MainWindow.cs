using System;
using System.Threading;
using Cairo;
using Gtk;
using ModelDrawing;
using ModelsObjectsLib;
using ReflectionsUtils;
using UI = Gtk.Builder.ObjectAttribute;
using Model = ModelRun.ModelRun;

namespace Task08GUI
{
    class MainWindow : Window
    {
        [UI] private DrawingArea _drawArea;

        private DrawManager DrawManager = new DrawManager(new Reflections(
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

            _drawArea.Drawn += Draw;
            _drawArea.ShowAll();
        }

        private void Draw(object o, DrawnArgs args)
        {
            foreach (var kv in ModelManager.AllModelObjects)
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
            

            Model modelRun = new Model(
                5, 
                60, 
                95,
                ModelManager, 
                new Vector(50, 40),
                new Vector(240, 300));
            modelRun.Run();
            Thread t = new Thread(() =>
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