namespace Lingva.BC.Auth
{
    public interface IAuthService
    {
        string Authenticate(AuthRequest authRequest);
    }
}
