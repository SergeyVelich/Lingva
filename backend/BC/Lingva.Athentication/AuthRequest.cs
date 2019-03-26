using System.Diagnostics.CodeAnalysis;

namespace Lingva.BC.Auth
{
    public class AuthRequest
    {
        [ExcludeFromCodeCoverage]
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
