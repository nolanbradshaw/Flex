using Flex.Parsers;
using Xunit;

namespace Flex.Tests.Parsers
{
    public class JsonParserTests
    {
        private static readonly string Json = @"{
          ""Redis"": {
            ""Password"": """",
            ""Port"": 5432
          },
          ""AllowedHosts"": ""*""
        }
        ";

        [Fact]
        public void Test_ParseToDictionary_ReturnsThreeValues()
        {
            var mappedDict = JsonParser.ParseToDictionary(Json);

            Assert.True(mappedDict.Count == 3);
            // Keys should be period delimited.
            Assert.True(mappedDict.ContainsKey("Redis.Password"));
            Assert.True(mappedDict.ContainsKey("Redis.Port"));
            Assert.True(mappedDict.ContainsKey("AllowedHosts"));
        }
    }
}
