using System.Security.Cryptography;
using System.Text;

namespace Lingva.BC.Crypto
{
    public class DefaultCryptoProvider : IDefaultCryptoProvider
    {
        const int SALT_BYTE_COUNT = 16;

        public byte[] GetHashHMACSHA512(string password, out byte[] salt)
        {
            byte[] computedHash;
            using (var hmac = new HMACSHA512())
            {
                salt = hmac.Key;
                computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }

            return computedHash;
        }

        public byte[] GetHashHMACSHA512(string password, byte[] salt)
        {
            byte[] computedHash;
            using (var hmac = new HMACSHA512(salt))
            {
                computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }

            return computedHash;
        }
    }
}
