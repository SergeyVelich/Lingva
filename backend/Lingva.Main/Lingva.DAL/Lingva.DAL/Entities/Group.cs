using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Lingva.DAL.Entities
{
    [ExcludeFromCodeCoverage]
    public class Group : BaseBE
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }

        public int LanguageId { get; set; }

        public virtual Language Language { get; set; }
        public virtual ICollection<GroupUser> GroupUsers { get; set; }
    }
}
