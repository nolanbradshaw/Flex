using Flex.Extensions;
using Flex.Parsers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.RepresentationModel;

namespace Flex.Helpers
{
    public static class FileHelpers
    {
        /// <summary>
        /// Reads a configuration files text and converts to a Dictionary<string, string>
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static Dictionary<string, string> FileToDictionary(string filePath)
        {
            var fileExt = Path.GetExtension(filePath);
            var fileText = File.ReadAllText(filePath);

            if (fileExt == ".json")
            {
                return JsonParser.ParseToDictionary(fileText);
            }
            else
            {
                using var streamReader = new StreamReader(filePath);
                var yamlStream = streamReader.ToYamlStream();

                var mappingNode = (YamlMappingNode)yamlStream.Documents[0].RootNode;
                return YamlParser.ParseToDictionary(mappingNode);
            }
        }
    }
}
