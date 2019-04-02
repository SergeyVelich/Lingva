using System.Diagnostics.CodeAnalysis;

namespace Lingva.WebAPI.Models.Request
{
    [ExcludeFromCodeCoverage]
    public class FilterViewModel
    {
        public int Language { get; set; }
        public string Name { get; set; }
    }
}
