using Microsoft.AspNetCore.Identity;
using System.Diagnostics.CodeAnalysis;

namespace AuthServer.Identity.Entities
{
    [ExcludeFromCodeCoverage]
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
