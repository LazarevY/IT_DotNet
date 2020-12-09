using System.Collections.Generic;

namespace ModelsLib
{
    public class BigLoader : ILoader
    {
        private readonly Storage _storage = new Storage(60);
        public bool IsFull => _storage.IsFull;

        public bool Load(Cargo cargo)
        {
            return _storage.Add(cargo);
        }

        public void Load(Storage storage)
        {
            List<Cargo> cargoes = new List<Cargo>();
            storage.Take(ref cargoes, _storage.Available);
            foreach (var cargo in cargoes)
            {
                _storage.Add(cargo);
            }
            ThrowMessage?.Invoke($"Cargo loaded({cargoes.Count})");
        }

        public void Unload(Storage storage)
        {
            List<Cargo> cargoes = new List<Cargo>();
            _storage.Take(ref cargoes, _storage.Filled);
            ThrowMessage?.Invoke($"Cargo unloaded({cargoes.Count})");
            
        }

        public event ILogable.MessageHandler ThrowMessage;
    }
}