using System.IO;
using YamlDotNet.RepresentationModel;

namespace Flex.Extensions
{
    internal static class YamlExtensions
    {
        internal static YamlStream ToYamlStream(this StreamReader streamReader)
        {
            var yamlStream = new YamlStream();
            yamlStream.Load(streamReader);
            return yamlStream;
        }
    }
}
