using Microsoft.AspNetCore.Identity;
using System.Diagnostics.CodeAnalysis;

namespace AuthServer.Identity.Entities
{
    [ExcludeFromCodeCoverage]
    public class AppUser : IdentityUser
    {
        // Add additional profile data for application users by adding properties to this class
        public string Name { get; set; }
    }
}
