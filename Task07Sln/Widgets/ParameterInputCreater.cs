using System;
using System.Collections.Generic;

namespace Task07GUI
{
    public class ParameterInputCreater
    {
        private Dictionary<Type, Type> WidgetDictionary;

        public ParameterInputCreater()
        {
            WidgetDictionary = new Dictionary<Type, Type>
            {
                {typeof(int), typeof(IntegerValueWidget)},
                {typeof(uint), typeof(UnsignedValueWidget)},
                {typeof(double), typeof(DoubleValueWidget)},
                {typeof(string), typeof(StringValueWidget)},
                {typeof(bool), typeof(BooleanGetWidget)},
                {typeof(object), typeof(ObjectValueWidget)}
            };
        }
        
        public IGetValueWidget Create(Type type)
        {
            Type target;
            bool ok = WidgetDictionary.TryGetValue(type, out target);
            if (ok)
                return (IGetValueWidget) Activator.CreateInstance(target);
            throw new ArgumentException($"No widget for type {type}");
        }

    }
}