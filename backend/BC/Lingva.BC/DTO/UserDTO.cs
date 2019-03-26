using System.Diagnostics.CodeAnalysis;

namespace Lingva.BC.DTO
{
    [ExcludeFromCodeCoverage]
    public class UserDTO
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
