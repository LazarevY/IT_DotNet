using System;

namespace ModelsLib
{
    public class MilkFarm : IModel, ILogable, IProcessLoader
    {
        public delegate void EquipmentHandler(Equipment equipment);

        public delegate void MilkHandler(string msg);


        private readonly Random _random = new Random();
        private Storage _internalStorage = new Storage(10);
        private bool _mechanicCalled;

        private bool _releaseCalled;

        public Storage MilkStorage { get; set; }

        private MilkEquipment Equipment { get; } = new MilkEquipment();


        public int GenChance { get; set; } = 40;

        public int BrokeChance { get; set; }

        public event ILogable.MessageHandler ThrowMessage;


        public void Update(uint ticks = 1)
        {
            GenerateMilk();
        }

        public void Process(ILoader loader)
        {
            loader.Load(MilkStorage);
        }

        public event EquipmentHandler EquipmentBroken;

        private void GenerateMilk()
        {
            if (MilkStorage.IsFull)
            {
                ThrowMessage?.Invoke("Некуда!");
                if (_releaseCalled) return;
                MilkStorage.Release();
                _releaseCalled = true;
                return;
            }

            _releaseCalled = false;


            if (!Equipment.Enabled)
            {
                ThrowMessage?.Invoke("Наташа мы все уронили");
                if (_mechanicCalled) return;
                _mechanicCalled = true;
                EquipmentBroken?.Invoke(Equipment);
                return;
            }

            _mechanicCalled = false;


            if (_random.Next(1, 100) > GenChance) return;
            GenerateNewMilk();


            ThrowMessage?.Invoke($"Storage filled: {MilkStorage.Filled}");
            if (_random.Next(1, 100) > BrokeChance) return;
            BreakEquipment();
        }

        private void BreakEquipment()
        {
            Equipment.Strength -= _random.Next(10, 20);
            ThrowMessage?.Invoke($"Strench : {Equipment.Strength}");
        }

        private void GenerateNewMilk()
        {
            ThrowMessage?.Invoke("Dancing polish cow say: hey?");
            var milk = new Milk();
            MilkStorage.Add(milk);
        }
    }
}