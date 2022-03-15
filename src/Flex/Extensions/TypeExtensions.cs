using System;
using System.Collections.Generic;
using System.Reflection;

namespace Flex.Extensions
{
    public static class TypeExtensions
    {
        public static void SetPropertyValue(this Type type, string propertyName, object value)
        {
            var property = type.GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);
            if (property != null && property.CanWrite)
            {
                property.SetValue(type, Convert.ChangeType(value, property.GetType()), null);
            }
        }

        public static HashSet<string> GetPropertiesLookup(this Type type)
        {
            HashSet<string> lookup = new();
            foreach(var property in type.GetProperties())
            {
                lookup.Add(property.Name);
            }

            return lookup;
        }
    }
}
