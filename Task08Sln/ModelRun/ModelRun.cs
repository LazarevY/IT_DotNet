﻿using System;
using System.Threading;
using ModelsLib;
using ModelsObjectsLib;

namespace ModelRun
{
    public class ModelRun
    {
        public ModelRun(
            int storageCapacity,
            int milkGenChance,
            int brokeChance,
            ModelManager modelManager,
            Vector farmLocation,
            Vector farmStorageLocation)
        {
            ModelManager = modelManager;
            var storage = new Storage(storageCapacity);
            StorageObject = new StorageObject
            {
                Location = farmStorageLocation,
                ProcessLoader = storage
            };

            var farm = new MilkFarm
            {
                MilkStorage = storage,
                GenChance = milkGenChance,
                BrokeChance = brokeChance
            };
            farm.ThrowMessage += Console.WriteLine;
            farm.EquipmentBroken += equipment =>
            {
                var acquireMechanic = modelManager.AcquireMechanic();
                acquireMechanic.SetTargetEquipment(equipment);
            };
            FarmObject = new FarmObject
            {
                Farm = farm,
                Location = farmLocation
            };

            StorageObject.FullStorageObject += processObject =>
            {
                var acquireLoader = modelManager.AcquireLoader();
                acquireLoader.GoToObject(processObject);
            };

            ModelThread = new Thread(() =>
            {
                while (true)
                {
                    FarmObject.Update();
                    StorageObject.Update();
                    Thread.Sleep(500);
                }
            });
        }

        public Thread ModelThread { get; }
        public FarmObject FarmObject { get; set; }
        public StorageObject StorageObject { get; set; }
        public ModelManager ModelManager { get; set; }

        public void Run()
        {
            ModelThread.Start();
        }
    }
}