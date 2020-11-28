using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ReflectionsUtils
{
    public class Reflections
    {
        public string PathToScan { get; }
        private Assembly Assembly { get; set; }

        public Reflections(string pathToScan)
        {
            PathToScan = pathToScan;
            Assembly = Assembly.LoadFrom(PathToScan);
        }

        public List<Type> AllImplementsOf(Type type)
        {
            if (!type.IsInterface)
                return new List<Type>();
            return new List<Type>(Assembly.GetTypes()
                .Where(t => UsualClass(t) && InheritsFrom(t, type)));
        }

        public bool UsualClass(Type type)
        {
            return type.IsClass && !type.IsAbstract;
        }
        public bool InheritsFrom(Type type, Type baseType)
        {
            // null does not have base type
            if (type == null)
            {
                return false;
            }

            // only interface or object can have null base type
            if (baseType == null)
            {
                return type.IsInterface || type == typeof(object);
            }

            // check implemented interfaces
            if (baseType.IsInterface)
            {
                return type.GetInterfaces().Contains(baseType);
            }

            // check all base types
            var currentType = type;
            while (currentType != null)
            {
                if (currentType.BaseType == baseType)
                {
                    return true;
                }

                currentType = currentType.BaseType;
            }

            return false;
        }
    }
}