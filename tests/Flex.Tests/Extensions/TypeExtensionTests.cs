using Flex.Extensions;
using Flex.Tests.Settings;
using Xunit;

namespace Flex.Tests.Extensions
{
    public class TypeExtensionTests
    {
        [Fact]
        public void Test_SetNestedPropertyValue_SetsCorrectValue()
        {
            const string key = "Redis.Server";
            var value = (object)"localhost";
            var target = new AppSettings();

            target.SetNestedPropertyValue(key, value);

            Assert.Equal(value, target.Redis.Server);
        }

        [Fact]
        public void Test_SetPropertyValue_SetsCorrectValue()
        {
            const string key = "AllowedHosts";
            var value = (object)"*";
            var target = new AppSettings();

            target.SetPropertyValue(key, value);

            Assert.Equal(value, target.AllowedHosts);
        }

        [Fact]
        public void Test_SafeGetProperty_PropertyExists_ReturnsProperty()
        {
            const string propName = "AllowedHosts";
            var target = new AppSettings();
            var type = target.GetType();

            var propInfo = type.SafeGetProperty(propName);

            Assert.NotNull(propInfo);
            Assert.Equal(propName, propInfo.Name);
        }

        [Fact]
        public void Test_SafeGetProperty_PropertyDoesNotExists_ReturnsNull()
        {
            var target = new AppSettings();
            var type = target.GetType();

            var propInfo = type.SafeGetProperty("NonExistantProp");

            Assert.Null(propInfo);
        }

        [Fact]
        public void Test_GetPropertiesLookup_SetsCorrectNumberOfProperties()
        {
            var target = new AppSettings();
            var type = target.GetType();

            var lookup = type.GetPropertiesLookup();
            var propComparison = type.GetProperties();

            Assert.Equal(propComparison.Length, lookup.Count);
        }
    }
}
