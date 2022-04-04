using Flex.Extensions;
using Flex.Tests.Settings;
using System.Collections.Generic;
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

        [Fact]
        public void Test_GetBaseType_Int_ReturnsInt()
        {
            var testType = typeof(int);
            var returnedType = testType.GetBaseType();

            Assert.True(returnedType == typeof(int));
        }

        [Fact]
        public void Test_GetBaseType_IntEnumerable_ReturnsInt()
        {
            var testType = typeof(IEnumerable<int>);
            var returnedType = testType.GetBaseType();

            Assert.True(returnedType == typeof(int));
        }

        [Fact]
        public void Test_GetBaseType_IntList_ReturnsInt()
        {
            var testType = typeof(List<int>);
            var returnedType = testType.GetBaseType();

            Assert.True(returnedType == typeof(int));
        }


        [Fact]
        public void Test_GetBaseType_String_ReturnsString()
        {
            var testType = typeof(string);
            var returnedType = testType.GetBaseType();

            Assert.True(returnedType == typeof(string));
        }

        [Fact]
        public void Test_GetBaseType_StringEnumerable_ReturnsString()
        {
            var testType = typeof(IEnumerable<string>);
            var returnedType = testType.GetBaseType();

            Assert.True(returnedType == typeof(string));
        }

        [Fact]
        public void Test_GetBaseType_StringList_ReturnsString()
        {
            var testType = typeof(List<string>);
            var returnedType = testType.GetBaseType();

            Assert.True(returnedType == typeof(string));
        }

        [Fact]
        public void Test_GetBaseType_DoubleEnumerable_ReturnsDouble()
        {
            var testType = typeof(IEnumerable<double>);
            var returnedType = testType.GetBaseType();

            Assert.True(returnedType == typeof(double));
        }

        [Fact]
        public void Test_GetBaseType_DoubleList_ReturnsDouble()
        {
            var testType = typeof(List<double>);
            var returnedType = testType.GetBaseType();

            Assert.True(returnedType == typeof(double));
        }
    }
}
