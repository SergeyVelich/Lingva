using Lingva.BC.Auth;
using Lingva.DAL.UnitsOfWork.Contracts;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Lingva.BC.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWorkAuth _unitOfWork;
        private readonly IJwtSigningEncodingKey _signingEncodingKey;
        private readonly IOptions<AuthOptions> _authOptions;

        public AuthService(IUnitOfWorkAuth unitOfWork, IJwtSigningEncodingKey signingEncodingKey, IOptions<AuthOptions> authOptions)
        {
            _unitOfWork = unitOfWork;
            _signingEncodingKey = signingEncodingKey;
            _authOptions = authOptions;
        }

        public JwtToken Authenticate(AuthRequest authRequest)
        {           
            if (string.IsNullOrEmpty(authRequest.Login) || string.IsNullOrEmpty(authRequest.Password))
            {
                return null;
            }

            var user = _unitOfWork.Users.Get(x => x.Login == authRequest.Login);

            if (user == null)
            {
                return null;
            }

            if (!VerifyPasswordHash(authRequest.Password, user.PasswordHash))
            {
                return null;
            }

            ClaimsData claimsData = new ClaimsData
            { NameIdentifier = user.Id.ToString() };

            string tokenString = GetUserToken(claimsData);

            JwtToken token = new JwtToken()
            {
                Id = user.Id,
                Login = user.Login,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Token = tokenString
            };

            return token;
        }

        private static bool VerifyPasswordHash(string password, byte[] storedPasswordHash)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            }

            //using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            //{
            //    var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            //    for (int i = 0; i < computedHash.Length; i++)
            //    {
            //        if (computedHash[i] != storedPasswordHash[i]) return false;
            //    }
            //}

            return true;
        }

        public string GetUserToken(ClaimsData claimsData)
        {
            var claims = new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, claimsData.NameIdentifier)
            };

            var token = new JwtSecurityToken(
                issuer: "DemoApp",
                audience: "DemoAppClient",
                claims: claims,
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: new SigningCredentials(_signingEncodingKey.GetKey(), _signingEncodingKey.SigningAlgorithm)
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return tokenString;
        }
    }
}
