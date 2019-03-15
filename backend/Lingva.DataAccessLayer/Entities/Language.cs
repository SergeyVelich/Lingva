using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lingva.DAL.Entities
{
    public class Language
    {
        [Key]
        [StringLength(3)]
        public string Name { get; set; }

        public virtual ICollection<Word> Words { get; set; }
        public virtual ICollection<DictionaryRecord> UserDictionaryRecords { get; set; }

        public Language()
        {
            Words = new List<Word>();
            UserDictionaryRecords = new List<DictionaryRecord>();
        }
    }
}
