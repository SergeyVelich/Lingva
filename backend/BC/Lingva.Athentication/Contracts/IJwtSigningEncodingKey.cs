using Microsoft.IdentityModel.Tokens;

namespace Lingva.BC.Auth
{
    public interface IJwtSigningEncodingKey
    {
        string SigningAlgorithm { get; }

        SecurityKey GetKey();
    }
}
