using System;

namespace ModelsObjectsLib
{
    public abstract class ModelObject
    {
        public enum ObjectState
        {
            Active, Removed
        }
        
        public uint Id { get; private set; }

        public virtual int ZCoord { get; set; } = 0;

        public virtual void OnRemove()
        {
            
        }

        public ObjectState ObjState { get; set; } = ObjectState.Active;
        
        public Vector Location;
        public abstract bool InTheObjectArea(Vector vector);

        public virtual void Update(uint ticks = 1)
        {
            
        }
    }
}