using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Lingva.WebAPI.ViewModel.Request
{
    [ExcludeFromCodeCoverage]
    public class UserCreateViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
        [Display(Name = "First name")]
        public string FirstName { get; set; }
        [Display(Name = "Last name")]
        public string LastName { get; set; }
    }
}
