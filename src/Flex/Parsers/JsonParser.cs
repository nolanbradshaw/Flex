using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Flex.Parsers
{
    public static class JsonParser
    {
        public static Dictionary<string, object> ParseToDictionary(string json, string parentKey = "")
        {
            var jsonDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
            var dataDict = new Dictionary<string, object>();
            foreach (var item in jsonDict)
            {
                var key = string.IsNullOrEmpty(parentKey) ? item.Key : $"{parentKey}.{item.Key}";
                if (item.Value is JObject)
                {
                    var resultDict = ParseToDictionary(item.Value.ToString(), $"{key}");
                    resultDict.ToList().ForEach(x => dataDict.Add(x.Key, x.Value));
                }
                else if (item.Value is JArray array)
                {
                    var builder = new StringBuilder();
                    for (var i = 0; i < array.Count; i++)
                    {
                        builder.Append(array[i]);
                        if (i != array.Count - 1)
                        {
                            builder.Append(',');
                        }
                    }
                    
                    dataDict.Add(key, builder.ToString());
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
