using Lingva.BC.Common.Enums;

namespace Lingva.BC.DTO
{
    public class QuerySorterDTO
    {
        public string PropertyName { get; set; }
        public SortOrder SortOrder { get; set; }

        public QuerySorterDTO(string propertyName, SortOrder sortOrder)
        {
            PropertyName = propertyName;
            SortOrder = sortOrder;
        }
    }
}
