namespace ModelsLib
{
    public interface ILoader
    {
        bool Load(Cargo cargo);
        void Load(Storage storage);
        void Unload(Storage storage);
    }
}