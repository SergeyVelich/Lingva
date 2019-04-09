using System;
using System.Diagnostics.CodeAnalysis;

namespace Lingva.BC.DTO
{
    public class GroupDTO
    {
        [ExcludeFromCodeCoverage]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public int LanguageId { get; set; }
        public string Description { get; set; }
        public string Picture { get; set; }
    }
}
