﻿using System;
using System.Net;

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
        private bool _canWork = true;

        public Storage MilkStorage { get; set; }

        public MilkEquipment Equipment { get; } = new MilkEquipment();


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
            _canWork = true;
            if (!Equipment.Enabled)
            {
                ThrowMessage?.Invoke("Наташа мы все уронили");
                if (_mechanicCalled) return;
                _mechanicCalled = true;
                EquipmentBroken?.Invoke(Equipment);
                _canWork = false;
            }

            
            
            if (MilkStorage.IsFull)
            {
                ThrowMessage?.Invoke("Некуда!");
                if (_releaseCalled) return;
                MilkStorage.Release();
                _releaseCalled = true;
                _canWork = false;
            }
            
            if (!_canWork)
                return;
            
            _mechanicCalled = false;
            _releaseCalled = false;


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