using Lingva.BC.Common.Enums;
using System.Diagnostics.CodeAnalysis;

namespace Lingva.MVC.Models.Response.Group.Index
{
    [ExcludeFromCodeCoverage]
    public class IndexSortViewModel
    {
        public string Property { get; set; }
        public SortOrder Order { get; set; }
        public bool Up { get; set; }

        public IndexSortViewModel(string sortProperty, SortOrder sortOrder)
        {
            Property = sortProperty;
            Order = sortOrder;

            Up = sortOrder == SortOrder.Asc;
        }
    }
}
