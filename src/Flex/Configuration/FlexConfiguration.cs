using Flex.Extensions;
using Flex.Helpers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace Flex.Configuration
{
    public static class FlexConfiguration
    {
        /// <summary>
        /// Registers a FlexContainer with default files as a singleton.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static FlexContainer AddFlexContainer(this IServiceCollection services)
        {
            var dict = FileHelpers.DefaultFilesToDictionary();

            var container = new FlexContainer(dict);
            services.AddSingleton<IFlexContainer>(container);
            return container;
        }

        /// <summary>
        /// Registers a FlexContainer<T> as a singleton.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="services"></param>
        /// <returns></returns>
        public static FlexContainer<T> AddFlexContainer<T>(this IServiceCollection services)
            where T : class, new()
        {
            var dict = FileHelpers.DefaultFilesToDictionary();
            var obj = Activator.CreateInstance(typeof(T));
            dict.AddToObject(obj);

            var container = new FlexContainer<T>((T)obj);
            services.AddSingleton<IFlexContainer<T>>(container);
            return container;
        }

        /// <summary>
        /// Adds a configuration file to a FlexContainer.
        /// </summary>
        /// <param name="container"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static FlexContainer AddConfigFile(this FlexContainer container, string filePath)
        {
            Dictionary<string, string> dataDict = FileHelpers.FileToDictionary(filePath);

            foreach (var entry in dataDict)
            {
                if (!container.Data.ContainsKey(entry.Key))
                {
                    container.Data.Add(entry.Key, entry.Value);
                }
            }

            return container;
        }

        /// <summary>
        /// Adds a configuration file to a FlexContainer<T>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="container"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static FlexContainer<T> AddConfigFile<T>(this FlexContainer<T> container, string filePath)
            where T : class, new()
        {
            Dictionary<string, string> dataDict = FileHelpers.FileToDictionary(filePath);
            dataDict.AddToObject(container.Data);
            return container;
        }

        /// <summary>
        /// Adds environment variables to a FlexContainer.
        /// </summary>
        /// <param name="container"></param>
        /// <returns></returns>
        public static FlexContainer AddEnvironment(this FlexContainer container)
        {
            var envs = Environment.GetEnvironmentVariables();
            var dict = envs.ToStringDictionary();

            foreach (var entry in dict)
            {
                if (!container.Data.ContainsKey(entry.Key))
                {
                    container.Data.Add(entry.Key, entry.Value);
                }
            }

            return container;
        }

        /// <summary>
        /// Adds environment variables to a FlexContainer<T>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="container"></param>
        /// <returns></returns>
        public static FlexContainer<T> AddEnvironment<T>(this FlexContainer<T> container)
            where T : class, new()
        {
            var envs = Environment.GetEnvironmentVariables();
            envs.AddToObject(container.Data);
            return container;
        }
    }
}
