using Microsoft.AspNetCore.Identity;
using System.Diagnostics.CodeAnalysis;

namespace Lingva.DAL.Entities
{
    [ExcludeFromCodeCoverage]
    public class Account : IdentityUser
    {
        public byte[] Salt { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
