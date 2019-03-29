using Lingva.MVC.ViewModel.Response;
using System.Collections.Generic;

namespace Lingva.MVC.Models.Response
{
    public class IndexViewModel
    {
        public IEnumerable<GroupViewModel> Groups { get; set; }
        public SortViewModel SortViewModel { get; set; }
    }
}
