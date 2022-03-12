using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;

namespace Flex.Parsers
{
    public static class JsonParser
    {
        public static Dictionary<string, string> ParseToDictionary(string json, string parentKey = "")
        {
            var jsonDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
            var dataDict = new Dictionary<string, string>();
            foreach (var item in jsonDict)
            {
                var key = string.IsNullOrEmpty(parentKey) ? item.Key : $"{parentKey}.{item.Key}";
                if (item.Value is JObject)
                {
                    var resultDict = ParseToDictionary(item.Value.ToString(), $"{key}");
                    resultDict.ToList().ForEach(x => dataDict.Add(x.Key, x.Value));
                }
                else
                {
                    dataDict.Add($"{key}", item.Value.ToString());
                }
            }

            return dataDict;
        }
    }
}
