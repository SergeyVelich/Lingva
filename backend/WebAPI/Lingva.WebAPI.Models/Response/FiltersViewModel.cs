using System.Diagnostics.CodeAnalysis;

namespace Lingva.WebAPI.Models.Response
{
    [ExcludeFromCodeCoverage]
    public class FiltersViewModel
    {
        public int Language { get; set; }
        public string Name { get; set; }
    }
}
