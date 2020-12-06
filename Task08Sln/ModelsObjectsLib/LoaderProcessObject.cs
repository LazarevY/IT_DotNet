using ModelsLib;

namespace ModelsObjectsLib
{
    public abstract class LoaderProcessObject: ModelObject, IProcessLoader
    {
        public virtual IProcessLoader ProcessLoader { get; set; }
        public virtual void Process(ILoader loader)
        {
            ProcessLoader.Process(loader);
        }
    }
}