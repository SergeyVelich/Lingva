using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Lingva.MVC.ViewModel.Request
{
    [ExcludeFromCodeCoverage]
    public class GroupCreateViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Group's title")]
        public string Title { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Display(Name = "Picture")]
        public string Picture { get; set; }
    }
}
