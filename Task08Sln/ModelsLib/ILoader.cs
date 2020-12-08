namespace ModelsLib
{
    public interface ILoader
    {
        
        bool IsFull { get;}
        bool Load(Cargo cargo);
        void Load(Storage storage);
        void Unload(Storage storage);
    }
}