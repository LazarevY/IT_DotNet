using System;
using System.Collections.Generic;
using ReflectionsUtils;

namespace ModelDrawing
{
    public class DrawManager
    {
        private Dictionary<Type, IModelDrawBase> _drawBase = new Dictionary<Type, IModelDrawBase>();
        public Reflections Reflections;

        public DrawManager(Reflections reflections)
        {
            Reflections = reflections;
            LoadDraw();
        }

        private void LoadDraw()
        {
            var allImplementsOf = Reflections.AllImplementsOf(typeof(IModelDrawBase));
            _drawBase.Clear();
            foreach (var type in allImplementsOf)
            {
                var @base = Activator.CreateInstance(type) as IModelDrawBase;
                _drawBase[@base.TypeOfObject()] = @base;
            }
        }

        public IModelDrawBase ForModel(Type type)
        {
            var modelDrawBase = _drawBase[type];
            return modelDrawBase;
        }
    }
}