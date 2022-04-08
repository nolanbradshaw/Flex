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
        /// <returns>Dictionary<string, string></returns>
        public static Dictionary<string, object> FileToDictionary(string filePath)
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

        /// <summary>
        /// Reads the default configuration files and converts to a Dictionary<string, string>
        /// </summary>
        /// <returns>Dictionary<string, string></returns>
        public static Dictionary<string, object> DefaultFilesToDictionary()
        {
            Dictionary<string, object> result = new();
            var defaultFiles = DefaultFiles.ToList();

            List<Dictionary<string, object>> dictList = new();
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
