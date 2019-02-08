using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lingva.Model
{
    public class VocabularyRecord
    {
        public int Id { get; set; }
        [Required]
        public User Owner { get; set; }
        [Required]
        public string OriginalText { get; set; }
        [Required]
        public string TranslationText { get; set; }
        [Required]
        public Languages OriginalLanguage { get; set; }
        [Required]
        public Languages TranslationLanguage { get; set; }
    }
}
