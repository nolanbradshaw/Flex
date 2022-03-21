using Flex.Configuration;
using Flex.Tests.Base;
using Flex.Tests.Settings;
using Xunit;

namespace Flex.Tests.Configuration
{
    public class FlexConfigurationTests : FlexTestBase
    {
        [Fact]
        public void Test_AddConfigFile_WithYamlFile_SetsCorrectValues()
        {
            var container = new FlexContainer();
            container.AddConfigFile(YamlFilePath);

            Assert.True(container.Get("Redis.Server") is "localhost");
            Assert.True(container.Get("Redis.Port") is "5432");
            Assert.True(container.Get("AllowedHosts") is "*");
        }

        [Fact]
        public void Test_AddConfigFile_ToClass_WithYamlFile_SetsCorrectValues()
        {
            var container = new FlexContainer<AppSettings>(new AppSettings());
            container.AddConfigFile(YamlFilePath);

            Assert.True(container.Data.AllowedHosts is "*");
            // TODO: Add nested objects from yaml files
            // Can't check if the nested Redis object is set correctly
            // Since Flex doesn't supported nested objects from yaml files yet
            // https://github.com/nolanbradshaw/Flex/issues/8
        }

        [Fact]
        public void Test_AddConfigFile_WithYmlFile_SetsCorrectValues()
        {
            var container = new FlexContainer();
            container.AddConfigFile(YmlFilePath);

            Assert.True(container.Get("Redis.Server") is "localhost");
            Assert.True(container.Get("Redis.Port") is "5432");
            Assert.True(container.Get("AllowedHosts") is "*");
        }

        [Fact]
        public void Test_AddConfigFile_ToClass_WithYmlFile_SetsCorrectValues()
        {
            var container = new FlexContainer<AppSettings>(new AppSettings());
            container.AddConfigFile(YmlFilePath);

            Assert.True(container.Data.AllowedHosts is "*");
            // TODO: Add nested objects from files
            // Can't check if the nested Redis object is set correctly
            // Since Flex doesn't supported nested objects from files yet
            // https://github.com/nolanbradshaw/Flex/issues/8
        }

        [Fact]
        public void Test_AddConfigFile_WithJsonFile_SetsCorrectValues()
        {
            var container = new FlexContainer();
            container.AddConfigFile(JsonFilePath);

            Assert.True(container.Get("Redis.Server") is "localhost");
            Assert.True(container.Get("Redis.Port") is "5432");
            Assert.True(container.Get("AllowedHosts") is "*");
        }

        [Fact]
        public void Test_AddConfigFile_ToClass_WithJsonFile_SetsCorrectValues()
        {
            var container = new FlexContainer<AppSettings>(new AppSettings());
            container.AddConfigFile(JsonFilePath);

            Assert.True(container.Data.AllowedHosts is "*");
            // TODO: Add nested objects from files
            // Can't check if the nested Redis object is set correctly
            // Since Flex doesn't supported nested objects from files yet
            // https://github.com/nolanbradshaw/Flex/issues/8
        }
    }
}
