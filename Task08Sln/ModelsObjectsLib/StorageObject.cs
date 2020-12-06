using ModelsLib;

namespace ModelsObjectsLib
{
    public class StorageObject: LoaderProcessObject
    {

        public Storage Storage { get; set; }

        public delegate void FullStorageObjectHandler(LoaderProcessObject processObject);

        public event FullStorageObjectHandler FullStorageObject;
            

        public override IProcessLoader ProcessLoader
        {
            get => Storage;
            set
            {
                Storage = (Storage) value;
                Storage.FullStorage += (storage) =>
                {
                    FullStorageObject?.Invoke(this);
                };
            }
        }

        public override bool InTheObjectArea(Vector vector)
        {
            return Location.Subtract(vector).Norm() < 10;
        }
    }
}