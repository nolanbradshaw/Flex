using Flex.Extensions;
using Flex.Parsers;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.IO;
using YamlDotNet.RepresentationModel;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Flex.Configuration
{
    public static class FlexConfiguration
    {
        public static void AddFlexContainer(this IServiceCollection services, string filePath)
        {
            using var streamReader = new StreamReader(filePath);
            var yamlStream = streamReader.ToYamlStream();

            var mappingNode = (YamlMappingNode)yamlStream.Documents[0].RootNode;
            services.AddSingleton<IFlexContainer>(new FlexContainer(YamlParser.ParseToDictionary(mappingNode)));
        }

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
    }
}
