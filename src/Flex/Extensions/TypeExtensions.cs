using System;
using System.Collections;
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
                var type = property.PropertyType.GetBaseType();
                var method = typeof(TypeExtensions).GetMethod(nameof(TypeExtensions.SetGenericPropertyValue));
                var generic = method.MakeGenericMethod(type);
                var result = generic.Invoke(obj, new object[] { obj, property, value });
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

        public static void SetGenericPropertyValue<T>(object obj, PropertyInfo propInfo, object value)
        {
            if (propInfo.PropertyType != typeof(string) && typeof(IEnumerable).IsAssignableFrom(propInfo.PropertyType))
            {
                List<T> listValue = value.ToString()
                    .Split(',')
                    .Select(x => (T)Convert.ChangeType(x, typeof(T), null))
                    .ToList();
                propInfo.SetValue(obj, listValue, null);
            }
            else
            {
                propInfo.SetValue(obj, Convert.ChangeType(value, propInfo.PropertyType), null);
            }
        }

        public static Type GetBaseType(this Type type)
        {
            if (type == typeof(string))
            {
                return type;
            }

            if (type.IsArray)
            {
                return type.GetElementType();
            }

            // type is IEnumerable<T>;
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IEnumerable<>))
            {
                return type.GetGenericArguments()[0];
            }

            // type implements/extends IEnumerable<T>;
            var enumerableType = type.GetInterfaces()
                                    .Where(t => t.IsGenericType &&
                                           t.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                                    .Select(t => t.GenericTypeArguments[0])
                                    .FirstOrDefault();

            return enumerableType ?? type;
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
