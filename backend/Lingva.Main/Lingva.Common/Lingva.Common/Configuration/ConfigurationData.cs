using System.Diagnostics.CodeAnalysis;

namespace Lingva.Common.Configuration
{
    [ExcludeFromCodeCoverage]
    public class ConfigurationData
    {
        public class SqlConnection
        {
            public string ConnectionString { get; set; }
        }
        public string AllowedHosts { get; set; }
    }
}
