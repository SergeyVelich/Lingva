using Lingva.BC.Common.Enums;

namespace Lingva.WebAPI.Models.Request
{
    public class SorterItemViewModel
    {
        public string Name { get; set; }
        public SortOrder SortOrder { get; set; }
        public bool IsFirst { get; set; }
    }
}
