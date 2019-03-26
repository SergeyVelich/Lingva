using Lingva.BC.Auth;
using Lingva.BC.Crypto;
using Lingva.DAL.Entities;
using Lingva.DAL.UnitsOfWork.Contracts;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Lingva.BC.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWorkAuth _unitOfWork;
        private readonly IJwtSigningEncodingKey _signingEncodingKey;
        private readonly IDefaultCryptoProvider _defaultCryptoProvider;
        private readonly IOptions<AuthOptions> _authOptions;

        public AuthService(IUnitOfWorkAuth unitOfWork, IJwtSigningEncodingKey signingEncodingKey, IDefaultCryptoProvider defaultCryptoProvider, IOptions<AuthOptions> authOptions)
        {
            _unitOfWork = unitOfWork;
            _signingEncodingKey = signingEncodingKey;
            _defaultCryptoProvider = defaultCryptoProvider;
            _authOptions = authOptions;
        }

        public string Authenticate(AuthRequest authRequest)
        {           
            if (string.IsNullOrEmpty(authRequest.Login) || string.IsNullOrEmpty(authRequest.Password))
            {
                return null;
            }

            User user = _unitOfWork.Users.Get(x => x.Login == authRequest.Login);

            if (user == null)
            {
                return null;
            }

            if (!VerifyPasswordHash(authRequest.Password, user.Salt, user.PasswordHash))
            {
                return null;
            }

            ClaimsData claimsData = new ClaimsData
            { NameIdentifier = user.Id.ToString() };

            string token = GetUserToken(claimsData);

            return token;
        }

        private bool VerifyPasswordHash(string password, byte[] salt, byte[] storedPasswordHash)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            }

            byte[] computedHash = _defaultCryptoProvider.GetHashHMACSHA512(password, salt);
            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != storedPasswordHash[i])
                {
                    return false;
                }
            }

            return true;
        }

        private string GetUserToken(ClaimsData claimsData)
        {
            Claim[] claims = new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, claimsData.NameIdentifier)
            };

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _authOptions.Value.Issuer,
                audience: _authOptions.Value.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_authOptions.Value.Lifetime),
                signingCredentials: new SigningCredentials(_signingEncodingKey.GetKey(), _signingEncodingKey.SigningAlgorithm)
            );

            string tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return tokenString;
        }
    }
}
