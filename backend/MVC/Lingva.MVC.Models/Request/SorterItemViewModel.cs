using Lingva.BC.Common.Enums;
using Lingva.MVC.Models.Contracts;
using System.Collections.Generic;

namespace Lingva.MVC.Models.Request
{
    public class SorterItemViewModel : IHttpParametersSource
    {
        public string Name { get; set; }
        public SortOrder SortOrder { get; set; }
        public bool IsFirst { get; set; }

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
