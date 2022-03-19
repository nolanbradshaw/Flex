using System;
using System.Collections;
using System.Collections.Generic;

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
                if (!properties.Contains(entry.Key.ToString()))
                {
                    continue;
                }

                obj.SetPropertyValue(entry.Key.ToString(), entry.Value);
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
