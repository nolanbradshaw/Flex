using System.Collections.Generic;
using YamlDotNet.RepresentationModel;

namespace Flex.Parsers
{
    public static class YamlParser
    {
        public static Dictionary<string, object> ParseToDictionary(YamlMappingNode mappingNode)
        {
            var dataDict = new Dictionary<string, object>();
            foreach (var child in mappingNode.Children)
            {
                if (child.Value is YamlMappingNode node)
                {
                    foreach (var nodeChild in node.Children)
                    {
                        var key = $"{child.Key}.{nodeChild.Key}";
                        dataDict.Add(key, nodeChild.Value.ToString());
                    }
                }
                else
                {
                    dataDict.Add(child.Key.ToString(), child.Value.ToString());
                }
            }

            return dataDict;
        }
    }
}
