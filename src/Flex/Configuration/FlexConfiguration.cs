using Flex.Extensions;
using Flex.Parsers;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using YamlDotNet.RepresentationModel;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Flex.Configuration
{
    public static class FlexConfiguration
    {
        /// <summary>
        /// Register container from a file.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="filePath"></param>
        public static void AddFlexContainer(this IServiceCollection services, string filePath)
        {
            var fileExt = Path.GetExtension(filePath);
            var fileText = File.ReadAllText(filePath);

            Dictionary<string, string> dataDict = new();
            if (fileExt == ".json")
            {
                dataDict = JsonParser.ParseToDictionary(fileText);
            }
            else
            {
                using var streamReader = new StreamReader(filePath);
                var yamlStream = streamReader.ToYamlStream();

                var mappingNode = (YamlMappingNode)yamlStream.Documents[0].RootNode;
                dataDict = YamlParser.ParseToDictionary(mappingNode);
            }

            services.AddSingleton<IFlexContainer>(new FlexContainer(dataDict));
        }

        /// <summary>
        /// Register container from a file.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="services"></param>
        /// <param name="filePath"></param>
        public static void AddFlexContainer<T>(this IServiceCollection services, string filePath)
            where T : class
        {
            var fileExt = Path.GetExtension(filePath);
            var fileText = File.ReadAllText(filePath);

            T obj = null;
            if (fileExt == ".json")
            {
                obj = JsonConvert.DeserializeObject<T>(fileText);
            }
            else
            {
                var deserializer = new DeserializerBuilder()
                    .WithNamingConvention(UnderscoredNamingConvention.Instance)
                    .Build();
                obj = deserializer.Deserialize<T>(fileText);
            }

            // Check for obj being null
            services.AddSingleton<IFlexContainer<T>>(new FlexContainer<T>(obj));
        }

        /// <summary>
        /// Register container with environment variables.
        /// </summary>
        /// <param name="services"></param>
        public static void AddFlexContainer(this IServiceCollection services)
        {
            Dictionary<string, string> dataDict = new();

            var envs = Environment.GetEnvironmentVariables();
            foreach(DictionaryEntry variable in envs)
            {
                dataDict.Add(variable.Key.ToString(), variable.Value.ToString());
            }

            services.AddSingleton<IFlexContainer>(new FlexContainer(dataDict));
        }
    }
}
