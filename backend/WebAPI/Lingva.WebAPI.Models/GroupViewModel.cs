using System.Diagnostics.CodeAnalysis;

namespace Lingva.WebAPI.ViewModel
{
    [ExcludeFromCodeCoverage]
    public class GroupViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Picture { get; set; }
    }
}
