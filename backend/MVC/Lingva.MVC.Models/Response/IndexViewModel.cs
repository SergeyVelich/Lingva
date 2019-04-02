using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Lingva.MVC.Models.Response
{
    [ExcludeFromCodeCoverage]
    public class IndexViewModel
    {
        public IEnumerable<GroupViewModel> Groups { get; set; }
        public IndexPageViewModel PageViewModel { get; set; }
        public IndexFilterViewModel FilterViewModel { get; set; }
        public IndexSortViewModel SortViewModel { get; set; }
    }
}
