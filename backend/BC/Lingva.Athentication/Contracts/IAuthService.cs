namespace Lingva.BC.Auth
{
    public interface IAuthService
    {
        JwtToken Authenticate(AuthRequest authRequest);
    }
}
