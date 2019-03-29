using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lingva.DAL.Entities
{
    public class Language
    {
        [Key]
        public string Name { get; set; }
        public virtual ICollection<Group> Groups { get; set; }

        public Language()
        {
            Groups = new List<Group>();
        }
    }
}
