using System;
using System.Collections.Generic;

namespace ModelsLib
{
    public class Storage: IProcessLoader
    {
        public delegate void FullStorageHandler(Storage storage);

        private readonly List<Cargo> _cargoes = new List<Cargo>();
        private int _available;
        private int _filled;

        public Storage(int capacity)
        {
            Capacity = capacity;
            Available = capacity;
        }

        public int Capacity { get; }

        public bool IsFull => Filled == Capacity;

        public bool IsNotEmpty => Available < Capacity;

        public int Available
        {
            get => Capacity - _filled;
            private set
            {
                if (value > Capacity)
                    throw new ArgumentException($"Invalid value {value}: Capacity is {Capacity}");
                _available = value;
                _filled = Capacity - _available;
            }
        }

        public int Filled
        {
            get => Capacity - _available;
            private set
            {
                if (value > Capacity)
                    throw new ArgumentException($"Invalid value {value}: Capacity is {Capacity}");
                _filled = value;
                _available = Capacity - _filled;
            }
        }

        public void Release()
        {
            FullStorage?.Invoke(this);
        }

        public event FullStorageHandler FullStorage;

        public bool CanAdd(Cargo cargo)
        {
            return Available >= cargo.Capacity;
        }

        public bool Add(Cargo cargo)
        {
            if (!CanAdd(cargo))
            {
                if (IsFull) Release();

                return false;
            }

            Available -= cargo.Capacity;
            _cargoes.Add(cargo);

            return true;
        }

        public bool Take(ref List<Cargo> cargoes, int maxCapacity)
        {
            var ret = false;
            for (var i = _cargoes.Count - 1; i >= 0 && maxCapacity > 0; --i)
                if (_cargoes[i].Capacity <= maxCapacity)
                {
                    ret = true;
                    var c = _cargoes[i];
                    Filled -= c.Capacity;
                    _cargoes.RemoveAt(i);
                    maxCapacity -= c.Capacity;
                    cargoes.Add(c);
                }

            return ret;
        }

        public void Process(ILoader loader)
        {
            loader.Load(this);
        }
    }
}