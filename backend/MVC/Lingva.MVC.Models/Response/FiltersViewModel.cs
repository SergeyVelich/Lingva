using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Lingva.MVC.Models.Response
{
    [ExcludeFromCodeCoverage]
    public class FiltersViewModel
    {
        public int? Language { get; set; }
        public string Name { get; set; }

        public Dictionary<string, object> GetPropertyAsDictionary()
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
