using System.Collections.Generic;

namespace Flex.Constants
{
    public static class DefaultFiles
    {
        public const string JsonAppSettings = "appsettings.json";

        public const string YamlAppSettings = "appsettings.yaml";

        public const string YmlAppSettings = "appsettings.yml";

        public static List<string> ToList()
        {
            return new List<string>()
            {
                JsonAppSettings,
                YamlAppSettings,
                YmlAppSettings
            };
        }
    }
}
