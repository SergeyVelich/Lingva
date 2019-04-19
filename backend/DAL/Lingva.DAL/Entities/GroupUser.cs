using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Lingva.DAL.Entities
{
    [ExcludeFromCodeCoverage]
    public class GroupUser : BaseBE
    {
        public int GroupId { get; set; }
        public Group Group { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
