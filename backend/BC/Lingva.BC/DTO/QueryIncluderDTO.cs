using Lingva.BC.Common.Enums;

namespace Lingva.BC.DTO
{
    public class QueryIncluderDTO
    {
        public string PropertyName { get; set; }

        public QueryIncluderDTO(string propertyName)
        {
            PropertyName = propertyName;
        }
    }
}
