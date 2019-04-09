using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Lingva.MVC.Models.Response
{
    [ExcludeFromCodeCoverage]
    public class IndexViewModel
    {
        public IEnumerable<GroupViewModel> Groups { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public FilterViewModel FilterViewModel { get; set; }
        public SortViewModel SortViewModel { get; set; }
    }
}
