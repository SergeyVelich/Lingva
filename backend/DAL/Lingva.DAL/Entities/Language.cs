using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Lingva.DAL.Entities
{
    [ExcludeFromCodeCoverage]
    public class Language : BaseBE
    {
        public string Name { get; set; }
        public virtual ICollection<Group> Groups { get; set; }

        public Language()
        {
            Groups = new List<Group>();
        }
    }
}
