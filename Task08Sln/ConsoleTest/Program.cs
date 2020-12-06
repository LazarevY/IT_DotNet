using System;
using System.Runtime.CompilerServices;
using System.Threading;
using ModelsLib;
using ModelsObjectsLib;
using Model = ModelRun.ModelRun;

namespace ConsoleTest
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            ModelManager modelManager = new ModelManager();
            

            Model modelRun = new Model(
                5, 
                60, 
                25,
                modelManager, 
                new Vector(0,0),
                new Vector(10, 0));
            
            Thread modelManageThread = new Thread(() =>
            {
                while (true)
                {
                    modelManager.Update();
                    Thread.Sleep(500);
                }
            });
            
            modelManageThread.Start();
            modelRun.Run();

        }
    }
}