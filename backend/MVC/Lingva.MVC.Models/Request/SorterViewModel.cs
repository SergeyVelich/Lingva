using Lingva.MVC.Models.Contracts;
using System.Collections.Generic;

namespace Lingva.MVC.Models.Request
{
    public class SorterViewModel : IHttpParametersSource
    {
        public List<SorterItemViewModel> SorterItems { get; set; }

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
