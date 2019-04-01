using System.Diagnostics.CodeAnalysis;

namespace Lingva.BC.DTO
{
    public class LanguageDTO
    {
        [ExcludeFromCodeCoverage]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
