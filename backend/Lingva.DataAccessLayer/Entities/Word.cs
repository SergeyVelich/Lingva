using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lingva.DAL.Entities
{
    public class Word
    {
        [Key]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        [StringLength(3)]
        public string LanguageName { get; set; }
        public virtual Language Language { get; set; }

        public virtual ICollection<DictionaryRecord> UserDictionaryRecords { get; set; }

        public Word()
        {
            UserDictionaryRecords = new List<DictionaryRecord>();
        }
    }
}
