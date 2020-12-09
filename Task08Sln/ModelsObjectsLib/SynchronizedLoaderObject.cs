using System;
using System.Threading;

namespace ModelsObjectsLib
{
    public class SynchronizedLoaderObject : LoaderObject
    {
        private Semaphore _semaphore  = new Semaphore(1,1);

        public void AcquireLoaderObject()
        {
            _semaphore.WaitOne();
        }

        public override void Update(uint ticks = 1)
        {
            switch (State)
            {
                case LoaderState.ToStorage:
                    MoveToTarget();
                    ChangeStateIfInTheAreaObject(LoaderState.Load);
                    break;
                case LoaderState.Load:
                    LoadAction();
                    break;
                case LoaderState.Return:
                case LoaderState.ReturnFull:
                    MoveToTarget();
                    if (LoaderBase.InTheObjectArea(Location))
                    {
                        LoaderBase.Process(Loader);
                        State = LoaderState.Wait;
                        try
                        {
                            _semaphore.Release();
                        }
                        catch (Exception e)
                        {
                            // ignored
                        }
                    }
                    break;
            }
        }

        protected override void LoadAction()
        {
            TargetObject.Process(Loader);
            TargetObject = LoaderBase;
            if (Loader.IsFull)
                State = LoaderState.ReturnFull;
            else
            {
                State = LoaderState.Return;
                try
                {
                    _semaphore.Release();
                }
                catch (Exception e)
                {
                    // ignored
                }
            }
        }
    }
}