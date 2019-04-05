using System.Collections.Generic;

namespace Lingva.MVC.Models.Request
{
    public class OptionsModel
    {
        public Dictionary<string, FilterModel> Filters { get; set; }
        public Dictionary<string, SorterModel> Sorters { get; set; }
        public PagenatorModel Pagenator { get; set; }
    }
}
