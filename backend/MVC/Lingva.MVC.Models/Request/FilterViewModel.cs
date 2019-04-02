using Lingva.MVC.Models.Contracts;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Lingva.MVC.Models.Request
{
    [ExcludeFromCodeCoverage]
    public class FilterViewModel : IHttpParametersSource
    {
        public int? Language { get; set; }
        public string Name { get; set; }

        public Dictionary<string, object> GetParametersDictionary()
        {
            Dictionary<string, object> properties = new Dictionary<string, object>();
            var publicProperties = this.GetType().GetProperties();
            foreach (var prop in publicProperties)
            {
                properties[prop.Name] = prop.GetValue(this);
            }

            return properties;
        }
    }
}
