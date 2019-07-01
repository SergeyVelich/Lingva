using System;
using System.Diagnostics.CodeAnalysis;

namespace Lingva.BC.Dto
{
    public class GroupDto
    {
        [ExcludeFromCodeCoverage]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public int LanguageId { get; set; }
        public string LanguageName { get; set; }
        public string Description { get; set; }
    }
}
