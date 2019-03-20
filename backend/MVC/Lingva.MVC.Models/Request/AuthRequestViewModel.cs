using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Lingva.MVC.ViewModel.Request
{
    [ExcludeFromCodeCoverage]
    public class AuthRequestViewModel
    {
        [Required]
        [Display(Name = "Login")]
        public string Login { get; set; }
        [Required]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}
