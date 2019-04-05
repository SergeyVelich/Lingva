using Lingva.BC.Common.Enums;

namespace Lingva.MVC.Models.Request
{
    public class SorterModel
    {
        public string SorterString { get; set; }
        public string PropertyName { get; set; }
        public SortOrder SortOrder { get; set; }
    }
}
