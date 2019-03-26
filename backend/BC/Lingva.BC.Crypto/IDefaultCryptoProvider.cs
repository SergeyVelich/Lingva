namespace Lingva.BC.Crypto
{
    public interface IDefaultCryptoProvider
    {
        byte[] GetHashHMACSHA512(string password, out byte[] salt);
        byte[] GetHashHMACSHA512(string password, byte[] salt);
    }
}
