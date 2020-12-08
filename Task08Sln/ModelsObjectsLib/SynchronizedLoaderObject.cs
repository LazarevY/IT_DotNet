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
        
        protected override void LoadAction()
        {
            TargetObject.Process(Loader);
            State = LoaderState.Return;
            TargetObject = LoaderBase;
            _semaphore.Release();
        }
    }
}