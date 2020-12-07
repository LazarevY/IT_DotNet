using System;
using ModelsLib;

namespace ModelsObjectsLib
{
    public class LoaderObject : ModelObject
    {
        public enum LoaderState
        {
            ToStorage,
            Return,
            Full,
            Wait,
            Load
        }

        public LoaderProcessObject LoaderBase{ get; set; }

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
                    MoveToTarget();
                    if (ChangeStateIfInTheAreaObject(LoaderState.Wait))
                        ObjState = ObjectState.Removed;
                    break;

            }
        }

        public bool GoToObject(LoaderProcessObject loaderProcessObject)
        {
            if (State == LoaderState.Full || State == LoaderState.Load || State == LoaderState.ToStorage)
                return false;
            TargetObject = loaderProcessObject;
            State = LoaderState.ToStorage;
            return true;
        }

        private void LoadAction()
        {
            TargetObject.Process(Loader);
            State = LoaderState.Return;
            TargetObject = LoaderBase;
        }

        private bool ChangeStateIfInTheAreaObject(LoaderState state)
        {
            if (TargetObject.InTheObjectArea(Location))
            {
                State = state;
                return true;
            }

            return false;
        }

        private void MoveToTarget()
        {
            var dir = TargetObject.Location.Subtract(Location);
            var distance = dir.Norm();
            Location = Location.Add(distance < Speed ? 
                dir.Normalized().Multiply(distance) : dir.Normalized().Multiply(Speed));
            Console.WriteLine($"Loader location: {Location}");
        }
    }
}