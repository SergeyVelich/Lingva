using System.Collections.Generic;

namespace Lingva.WebAPI.Models.Request
{
    public class OptionsModel
    {
        public ICollection<FilterModel> Filters { get; set; }
        public ICollection<SorterModel> Sorters { get; set; }
        public PagenatorModel Pagenator { get; set; }
    }
}
