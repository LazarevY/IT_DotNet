using System;

namespace Lib
{
    public class MilkFarm : IModel, ILogable, IProcessLoader
    {
        public delegate void EquipmentHandler(Equipment equipment);

        public delegate void MilkHandler(string msg);


        private readonly Random _random = new Random();
        private Storage _internalStorage = new Storage(10);

        public Storage MilkStorage { get; set; }

        private MilkEquipment Equipment { get; } = new MilkEquipment();


        public int GenChance { get; set; } = 40;

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
                MilkStorage.Release();
                return;
            }


            if (!Equipment.Enabled)
            {
                ThrowMessage?.Invoke("Наташа мы все уронили");
                EquipmentBroken?.Invoke(Equipment);
                return;
            }


            if (_random.Next(1, 100) > GenChance) return;
            GenerateNewMilk();


            ThrowMessage?.Invoke($"Storage filled: {MilkStorage.Filled}");
            if (_random.Next(1, 100) > 20) return;
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