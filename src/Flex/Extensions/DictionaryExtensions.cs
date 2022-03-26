using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Flex.Extensions
{
    public static class DictionaryExtensions
    {
        public static Dictionary<string, string> ToStringDictionary(this IDictionary dict)
        {
            // TODO: Do we actually want to return null here? 
            // Or would it be more logical to return an empty dictionary.
            if (dict == null || dict.Count < 1)
            {
                return null;
            }

            Dictionary<string, string> resultDict = new();
            foreach (DictionaryEntry entry in dict)
            {
                resultDict.Add(entry.Key.ToString(), entry.Value.ToString());
            }

            return resultDict;
        }

        public static Dictionary<string, string> MergeDictionaries(this Dictionary<string, string> dict, Dictionary<string, string> dictToMerge)
        {
            foreach(var entry in dictToMerge)
            {
                if (!dict.ContainsKey(entry.Key))
                {
                    dict.Add(entry.Key, entry.Value);
                }
            }

            return dict;
        }

        private static void Test(string key, object value, object target)
        {
            PropertyInfo propertyToSet;
            var bits = key.Split('.');
            if (key.Contains('.'))
            {
                for (var i = 0; i < bits.Length - 1; i++)
                {
                    var propertyToGet = target.GetType().GetProperty(bits[i]);
                    if (propertyToGet == null)
                    {
                        return;
                    }

                    var propertyValue = propertyToGet.GetValue(target, null);
                    if (propertyValue == null)
                    {
                        propertyValue = Activator.CreateInstance(propertyToGet.PropertyType);
                        propertyToGet.SetValue(target, propertyValue);
                    }
                    target = propertyToGet.GetValue(target, null);
                }
            }

            propertyToSet = target.GetType().GetProperty(bits.Last());
            if (propertyToSet != null)
            {
                propertyToSet.SetValue(target, Convert.ChangeType(value, propertyToSet.PropertyType), null);
            }
        }

        /// <summary>
        /// Applies a dictionaries values to an objects properties.
        /// </summary>
        public static void AddToObject<T>(this IDictionary dict, T obj)
            where T : class, new()
        {
            var type = obj.GetType();
            var properties = type.GetPropertiesLookup();
            foreach (DictionaryEntry entry in dict)
            {
                Test(entry.Key.ToString(), entry.Value, obj);
            }
        }

        /// <summary>
        /// Converts a dictionary to an object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dict"></param>
        /// <returns></returns>
        public static T ToObject<T>(this IDictionary dict)
            where T : class
        {
            var obj = Activator.CreateInstance(typeof(T));
            var objType = obj.GetType();

            var typePropLookup = objType.GetPropertiesLookup();
            foreach (DictionaryEntry entry in dict)
            {
                // If the environment variable is a property in the given type.
                if (typePropLookup.Contains(entry.Key.ToString()))
                {
                    obj.SetPropertyValue(entry.Key.ToString(), entry.Value);
                }
            }

            return (T)obj;
        }
    }
}
