using ModelsLib;

namespace ModelsObjectsLib
{
    public class LoaderBaseObject: LoaderProcessObject
    {
        public override bool InTheObjectArea(Vector vector)
        {
            return Location.Subtract(vector).Norm() < 2;
        }

        public override void Process(ILoader loader)
        {
            loader.Unload(new Storage(1000000));
        }
    }
}