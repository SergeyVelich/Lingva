using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Lingva.Common
{
    [ExcludeFromCodeCoverage]
    public static class Constants
    {
        public static string CONFIGURATION_DATA = "ConfigurationData";
        public static string SQL_CONNECT = "ConfigurationData:SqlConnection";
        public static string CONNECTION_STRING = "ConfigurationData:SqlConnection:ConnectionString";
    }
}
