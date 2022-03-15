using System;
using System.Collections.Generic;

namespace Flex.Extensions
{
    public static class TypeExtensions
    {
        public static void SetPropertyValue(this object obj, string propertyName, object value)
        {
            var property = obj.GetType().GetProperty(propertyName);
            if (property != null && property.CanWrite)
            {
                property.SetValue(obj, Convert.ChangeType(value, property.PropertyType), null);
            }
        }

        public static HashSet<string> GetPropertiesLookup(this Type type)
        {
            HashSet<string> lookup = new();
            foreach (var property in type.GetProperties())
            {
                lookup.Add(property.Name);
            }

            return lookup;
        }
    }
}
