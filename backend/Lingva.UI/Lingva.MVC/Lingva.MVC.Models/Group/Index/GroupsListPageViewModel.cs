using Lingva.MVC.Models.Entities;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Lingva.MVC.Models.Group.Index
{
    [ExcludeFromCodeCoverage]
    public class GroupsListPageViewModel
    {
        public IEnumerable<GroupViewModel> Groups { get; set; }
        public PagenatorViewModel PagenatorViewModel { get; set; }
        public FilterViewModel FilterViewModel { get; set; }
        public SortViewModel SortViewModel { get; set; }
    }
}
