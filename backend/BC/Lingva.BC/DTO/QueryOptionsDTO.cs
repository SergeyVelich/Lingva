using System.Collections.Generic;

namespace Lingva.BC.DTO
{
    public class QueryOptionsDTO
    {
        public ICollection<QueryFilterDTO> Filters { get; set; }
        public ICollection<QuerySorterDTO> Sorters { get; set; }
        public QueryPagenatorDTO Pagenator { get; set; }
        public IComparer<object> comparer { get; set; }
    }
}
