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
        
        private ConcurrentDictionary<uint, ModelObject> UpdateModelObjects = new ConcurrentDictionary<uint, ModelObject>();
        public ConcurrentDictionary<uint, ModelObject> AllModelObjects { get; } = new ConcurrentDictionary<uint, ModelObject>();

        private LoaderBaseObject LoaderBaseObject;
        public Reflections Reflections { get; set; } = new Reflections("");

        private uint _id = 1;

        public ModelManager()
        {
            LoaderBaseObject = CreateObject<LoaderBaseObject>();
            LoaderBaseObject.Location = new Vector(200, 50);
        }

        public T CreateObject<T>(bool needUpdate = true)
        {
            if (!Reflections.InheritsFrom(typeof(T), typeof(ModelObject)))
                throw new ArgumentException($"{typeof(T)} is not inherits {typeof(ModelObject)}");
            T obj = (T) Activator.CreateInstance(typeof(T));
            ModelObject modelObject = obj as ModelObject;
            typeof(ModelObject).GetProperty("Id")?.SetValue(modelObject, _id++);
            if (needUpdate)
                UpdateModelObjects.TryAdd(modelObject.Id, modelObject);
            AllModelObjects.TryAdd(modelObject.Id, modelObject);
            return obj;
        }

        public MechanicObject AcquireMechanic(bool needUpdate = true)
        {
            var mechanicObject = CreateObject<MechanicObject>(needUpdate);
            mechanicObject.Mechanic = new HardMechanic();
            return mechanicObject;
        }

        public LoaderObject AcquireLoader(bool needUpdate = true)
        {
            var loaderObject = CreateObject<LoaderObject>(needUpdate);
            loaderObject.Loader = new SmallLoader();
            loaderObject.Location = LoaderBaseObject.Location;
            loaderObject.Speed = 20;
            loaderObject.LoaderBase = LoaderBaseObject;
            loaderObject.TargetObject = LoaderBaseObject;
            loaderObject.State = LoaderObject.LoaderState.Wait;
            return loaderObject;
        }

        public void Update()
        {
            var removeKeys = UpdateModelObjects.Values.Where(o => o.ObjState == ModelObject.ObjectState.Removed)
                .Select(o => o.Id);
            foreach (var removeKey in removeKeys.ToArray())
            {
                UpdateModelObjects.TryRemove(removeKey, out _);
                AllModelObjects.TryRemove(removeKey, out _);
            }

            foreach (var kv in UpdateModelObjects)
            {
                kv.Value.Update();
            }
        }
    }
}