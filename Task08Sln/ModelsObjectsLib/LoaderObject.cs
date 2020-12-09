using System;
using ModelsLib;

namespace ModelsObjectsLib
{
    public class LoaderObject : ModelObject
    {
        public enum LoaderState
        {
            Wait = 0,
            Return = 1,
            Load = 2,
            ToStorage = 3,
            ReturnFull = 4
        }

        public LoaderProcessObject LoaderBase { get; set; }

        public override int ZCoord { get; set; } = -1;

        public ILoader Loader { get; set; }

        public LoaderProcessObject TargetObject { get; set; }

        public int Speed { get; set; } = 30;

        public LoaderState State { get; set; } = LoaderState.Wait;

        public override bool InTheObjectArea(Vector vector)
        {
            return Location.Subtract(vector).Norm() < 3;
        }

        public override void Update(uint ticks = 1)
        {
            base.Update(ticks);
            switch (State)
            {
                case LoaderState.ToStorage:
                    MoveToTarget();
                    ChangeStateIfInTheAreaObject(LoaderState.Load);
                    break;
                case LoaderState.Load:
                    LoadAction();
                    break;
                case LoaderState.Return:
                case LoaderState.ReturnFull:
                    MoveToTarget();
                    if (TargetObject.InTheObjectArea(Location))
                    {
                        LoaderBase.Process(Loader);
                        State = LoaderState.Wait;
                        
                    }
                    break;
            }
        }

        public bool GoToObject(LoaderProcessObject loaderProcessObject)
        {
            if (State == LoaderState.ReturnFull || State == LoaderState.Load || State == LoaderState.ToStorage)
                return false;
            TargetObject = loaderProcessObject;
            State = LoaderState.ToStorage;
            return true;
        }

        protected virtual void LoadAction()
        {
            TargetObject.Process(Loader);
            State = Loader.IsFull ? LoaderState.ReturnFull : LoaderState.Return;
            TargetObject = LoaderBase;
        }

        protected bool ChangeStateIfInTheAreaObject(LoaderState state)
        {
            if (TargetObject.InTheObjectArea(Location))
            {
                State = state;
                return true;
            }

            return false;
        }

        protected void MoveToTarget()
        {
            var dir = TargetObject.Location.Subtract(Location);
            var distance = dir.Norm();
            Location = Location.Add(distance < Speed
                ? dir.Normalized().Multiply(distance)
                : dir.Normalized().Multiply(Speed));
            //Console.WriteLine($"Loader location: {Location}");
        }
    }
}