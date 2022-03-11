using Flex.Parsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using YamlDotNet.RepresentationModel;

namespace Flex.Tests.Parsers
{
    public class YamlParserTests
    {
        private static readonly YamlMappingNode ChildMappingNode = new()
        {
            { "Server", "localhost" },
            { "Password", "Test" }
        };

        private static readonly YamlMappingNode MappingNode = new()
        {
            { "Version", "1.0.0" },
            { "IsOpenSource", "True" }
        };

        private static readonly YamlMappingNode MappingNodeWithChildNode = new()
        {
            { "Version", "1.0.0" },
            { "IsOpenSource", "True" },
            { "Redis", ChildMappingNode }
        };

        [Fact]
        public void Test_ParseToDictionary_ReturnsTwoValues()
        {
            var mappedDict = YamlParser.ParseToDictionary(MappingNode);

            Assert.True(mappedDict.Count == 2);
            Assert.Equal("Version", mappedDict.First().Key);
            Assert.Equal("IsOpenSource", mappedDict.ElementAt(1).Key);
        }

        [Fact]
        public void Test_ParseToDictionary_ParsesChildNode()
        {
            var mappedDict = YamlParser.ParseToDictionary(MappingNodeWithChildNode);

            // 2 "root" nodes and 2 "child" nodes
            Assert.True(mappedDict.Count == 4);
            // Ensure keys are . seperated
            Assert.Equal("Redis.Server", mappedDict.ElementAt(2).Key);
            Assert.Equal("Redis.Password", mappedDict.ElementAt(3).Key);
        }
    }
}
