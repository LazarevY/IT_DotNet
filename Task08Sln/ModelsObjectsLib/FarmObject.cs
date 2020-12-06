using ModelsLib;

namespace ModelsObjectsLib
{
    public class FarmObject : ModelObject
    {
        public MilkFarm Farm { get; set; }

        public override bool InTheObjectArea(Vector vector)
        {
            return Location.Subtract(vector).Norm() < 10;
        }

        public override void Update(uint ticks = 1)
        {
            base.Update(ticks);
            Farm.Update(ticks);
        }
    }
}