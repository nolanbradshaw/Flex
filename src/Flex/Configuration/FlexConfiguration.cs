using Flex.Extensions;
using Flex.Parsers;
using Microsoft.Extensions.DependencyInjection;
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
            var fileText = File.ReadAllText(filePath);
            var deserializer = new DeserializerBuilder()
                .WithNamingConvention(UnderscoredNamingConvention.Instance)
                .Build();

            services.AddSingleton(new FlexContainer<T>(deserializer.Deserialize<T>(fileText)));
        }
    }
}
