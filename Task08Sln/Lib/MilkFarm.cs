using System;
using System.Collections.Generic;

namespace Lib
{
    public class MilkFarm : IModel, ILogable
    {
        public delegate void EquipmentHandler(Equipment equipment);

        public delegate void MilkHandler(string msg);

        private readonly Random _random = new Random();

        public Storage MilkStorage { get; set; }
        private Storage _internalStorage = new Storage(10);

        private MilkEquipment Equipment { get; } = new MilkEquipment();


        public int GenChance { get; set; } = 40;

        public event ILogable.MessageHandler ThrowMessage;


        public void Update(uint ticks = 1)
        {
            GenerateMilk();
        }

        public event EquipmentHandler EquipmentBroken;

        private void GenerateMilk()
        {
            if (_internalStorage.IsFull)
            {
                ThrowMessage?.Invoke($"Некуда!");
                return;
            }

            if (_internalStorage.IsNotEmpty)
            {
                List<Cargo> milks = new List<Cargo>();
                _internalStorage.Take(ref milks, _internalStorage.Filled);
                while (true)
                {
                    if (milks.Count == 0)
                        break;
                    var c = milks[0];
                    if (!MilkStorage.Add(c))
                        break;
                    milks.RemoveAt(0);
                }
            }

            if (!Equipment.Enabled)
            {
                ThrowMessage?.Invoke("Наташа мы все уронили");
                EquipmentBroken?.Invoke(Equipment);
                return;
            }

            if (_random.Next(1, 100) > GenChance) return;
            ThrowMessage?.Invoke("Dancing polish cow say: why do you cum?");
            var milk = new Milk();
            if (!MilkStorage.Add(milk))
            {
                _internalStorage.Add(milk);
            }

            ThrowMessage?.Invoke($"Internal storage : {_internalStorage.Filled}. Storage: {MilkStorage.Filled}");


            if (_random.Next(1, 100) > 20) return;
            Equipment.Strength -= _random.Next(10, 20);
            ThrowMessage?.Invoke($"Strench : {Equipment.Strength}");
        }
    }
}