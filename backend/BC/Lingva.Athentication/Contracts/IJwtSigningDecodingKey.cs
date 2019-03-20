using Microsoft.IdentityModel.Tokens;

namespace Lingva.BC.Auth
{
    public interface IJwtSigningDecodingKey
    {
        SecurityKey GetKey();
    }
}
