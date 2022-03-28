using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

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

        public static void SetNestedPropertyValue(this object target, string key, object value)
        {
            var nestedKeys = key.Split('.');
            for (var i = 0; i < nestedKeys.Length - 1; i++)
            {
                var propertyToGet = target.GetType().SafeGetProperty(nestedKeys[i]);
                if (propertyToGet == null)
                {
                    // Exit since the prop path doesn't exist
                    return;
                }

                var propertyValue = propertyToGet.GetValue(target, null);
                if (propertyValue == null)
                {
                    // Create a new object to avoid exceptions when setting prop value.
                    propertyValue = Activator.CreateInstance(propertyToGet.PropertyType);
                    propertyToGet.SetValue(target, propertyValue);
                }

                target = propertyToGet.GetValue(target, null);
            }

            // The finally property name will always be the last split word.
            target.SetPropertyValue(nestedKeys.Last(), value);
        }

        public static PropertyInfo SafeGetProperty(this Type type, string propertyName)
        {
            try
            {
                return type.GetProperty(propertyName);
            } 
            catch (Exception)
            {
                return null;
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
