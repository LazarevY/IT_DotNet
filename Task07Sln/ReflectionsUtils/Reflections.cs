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

        public Type TypeByName(string name)
        {
            var enumerable = Assembly.GetTypes().Where(type => type.Name.Equals(name));
            List<Type> types = new List<Type>(enumerable);
            if (types.Count != 1)
                throw new ArgumentException($"Incorrect count classes of {name} : 0 or more than 1");
            return types[0];
        }
    }
}