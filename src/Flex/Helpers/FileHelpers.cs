using Flex.Constants;
using Flex.Extensions;
using Flex.Parsers;
using System;
using System.Collections.Generic;
using System.IO;
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

            if (fileExt is ".json")
            {
                return JsonParser.ParseToDictionary(fileText);
            }
            else if(fileExt is ".yml" or ".yaml")
            {
                using var streamReader = new StreamReader(filePath);
                var yamlStream = streamReader.ToYamlStream();

                var mappingNode = (YamlMappingNode)yamlStream.Documents[0].RootNode;
                return YamlParser.ParseToDictionary(mappingNode);
            }

            throw new ArgumentException("File type is not supported.");
        }

        public static Dictionary<string, string> DefaultFilesToDictionary()
        {
            Dictionary<string, string> result = new();
            var defaultFiles = DefaultFiles.ToList();

            List<Dictionary<string, string>> dictList = new();
            foreach(var file in defaultFiles)
            {
                if (File.Exists(file))
                {
                    dictList.Add(FileToDictionary(file));
                }
            }

            foreach(var dict in dictList)
            {
                result.MergeDictionaries(dict);
            }

            return result;
        }
    }
}
