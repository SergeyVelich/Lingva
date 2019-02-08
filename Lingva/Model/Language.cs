using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lingva.Model
{
    public class Language
    {
        public int Id { get; set; }
        [Required]
        public User Name { get; set; }
    }

    public enum Languages
    {
        en,
        ru,
    }
}
