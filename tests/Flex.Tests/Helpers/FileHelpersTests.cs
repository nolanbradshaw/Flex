using Flex.Constants;
using Flex.Helpers;
using System.IO;
using Xunit;

namespace Flex.Tests.Helpers
{
    public class FileHelpersTests
    {
        [Fact]
        public void Test_DefaultFilesToDictionary_ReturnsThreeValues()
        {
            var defaultFileDict = FileHelpers.DefaultFilesToDictionary();

            Assert.NotEmpty(defaultFileDict);
            Assert.True(defaultFileDict.Count == 3);
        }

        [Fact]
        public void Test_Yml_FileToDictionary_ReturnsThreeValues()
        {
            var fileDict = FileHelpers.FileToDictionary(DefaultFiles.YmlAppSettings);

            Assert.True(fileDict.Count == 3);
        }

        [Fact]
        public void Test_Yaml_FileToDictionary_ReturnsThreeValues()
        {
            var fileDict = FileHelpers.FileToDictionary(DefaultFiles.YamlAppSettings);

            Assert.True(fileDict.Count == 3);
        }

        [Fact]
        public void Test_Json_FileToDictionary_ReturnsThreeValues()
        {
            var fileDict = FileHelpers.FileToDictionary(DefaultFiles.JsonAppSettings);

            Assert.True(fileDict.Count == 3);
        }

        [Fact]
        public void Test_FileToDictionary_FileDoesntExist_ThrowsFileNotFoundException()
        {
            Assert.Throws<FileNotFoundException>(() => FileHelpers.FileToDictionary("nonexistant.json"));
        }
    }
}
