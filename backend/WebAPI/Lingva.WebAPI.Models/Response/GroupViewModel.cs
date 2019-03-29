using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Lingva.WebAPI.ViewModel.Response
{
    [ExcludeFromCodeCoverage]
    public class GroupViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Group's title")]
        public string Name { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Display(Name = "Picture")]
        public string Picture { get; set; }
    }
}
