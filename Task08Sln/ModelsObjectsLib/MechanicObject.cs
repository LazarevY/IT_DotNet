using System;
using ModelsLib;

namespace ModelsObjectsLib
{
    public class MechanicObject: ModelObject, IMechanic
    {
        
        public enum MechanicState
        {
            WaitTarget, 
            Target,
            Fix,
            FixDone
        }

        public IMechanic Mechanic { get; set; }

        public MechanicState State { get; private set; } = MechanicState.WaitTarget;
        
        public Equipment TargetEquipment { get; set; } = new MilkEquipment();
        

        public void SetTargetEquipment(Equipment equipment)
        {
            TargetEquipment = equipment;
            State = MechanicState.Target;
            Location = new Vector(60, 60);
        }
        public override bool InTheObjectArea(Vector vector)
        {
            return Location.Subtract(vector).Norm() < 2;
        }

        public void Fix(Equipment equipment)
        {
            Mechanic.Fix(equipment);
        }

        public override void OnRemove()
        {
            Console.WriteLine("Mechanic removed");
        }

        public override void Update(uint ticks = 1)
        {
            switch (State)
            {
                case MechanicState.Target:
                    State = MechanicState.Fix;
                    Console.WriteLine("Ready to fix");
                    break;
                case MechanicState.Fix:
                    Fix(TargetEquipment);
                    State = MechanicState.FixDone;
                    Console.WriteLine("Fix");
                    break;
                case MechanicState.FixDone:
                    ObjState = ObjectState.Removed;
                    Console.WriteLine("Fixed!");
                    break;
            }
        }
    }
}