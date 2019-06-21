using System.Diagnostics.CodeAnalysis;

namespace Lingva.DAL.Entities
{
    [ExcludeFromCodeCoverage]
    public class GroupUser : BaseBE
    {
        public int GroupId { get; set; }
        public virtual Group Group { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
