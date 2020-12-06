using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using ModelsLib;
using ReflectionsUtils;

namespace ModelsObjectsLib
{
    public class ModelManager
    {
        
        private ConcurrentDictionary<uint, ModelObject> ModelObjects = new ConcurrentDictionary<uint, ModelObject>();

        private LoaderBaseObject LoaderBaseObject { get; set; } = new LoaderBaseObject{Location = new Vector(40, 50)};
        public Reflections Reflections { get; set; } = new Reflections("");

        private uint _id = 1;

        public T CreateObject<T>()
        {
            if (!Reflections.InheritsFrom(typeof(T), typeof(ModelObject)))
                throw new ArgumentException($"{typeof(T)} is not inherits {typeof(ModelObject)}");
            T obj = (T) Activator.CreateInstance(typeof(T));
            ModelObject modelObject = obj as ModelObject;
            typeof(ModelObject).GetProperty("Id")?.SetValue(modelObject, _id++);
            ModelObjects.TryAdd(modelObject.Id, modelObject);
            return obj;
        }

        public MechanicObject AcquireMechanic()
        {
            var mechanicObject = CreateObject<MechanicObject>();
            mechanicObject.Mechanic = new HardMechanic();
            return mechanicObject;
        }

        public LoaderObject AcquireLoader()
        {
            var loaderObject = CreateObject<LoaderObject>();
            loaderObject.Loader = new SmallLoader();
            loaderObject.LoaderBase = LoaderBaseObject;
            loaderObject.TargetObject = LoaderBaseObject;
            loaderObject.State = LoaderObject.LoaderState.Wait;
            return loaderObject;
        }

        public void Update()
        {
            var removeKeys = ModelObjects.Values.Where(o => o.ObjState == ModelObject.ObjectState.Removed)
                .Select(o => o.Id);
            foreach (var removeKey in removeKeys.ToArray())
            {
                ModelObjects.TryRemove(removeKey, out _);
            }

            foreach (var kv in ModelObjects)
            {
                kv.Value.Update();
            }
        }
    }
}