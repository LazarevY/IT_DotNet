using System.Collections.Generic;

namespace ModelsLib
{
    public class InfinityLoader: ILoader
    {
        public bool Load(Cargo cargo)
        {
            return true;
        }

        public void Load(Storage storage)
        {
            var l = new List<Cargo>();
            storage.Take(ref l , storage.Filled);
        }

        public void Unload(Storage storage)
        {
            
        }
    }
}