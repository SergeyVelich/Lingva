using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Lingva.DAL.Entities
{
    [ExcludeFromCodeCoverage]
    public class Group
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string Picture { get; set; }

        public int LanguageId { get; set; }

        public virtual Language Language { get; set; }
    }
}
