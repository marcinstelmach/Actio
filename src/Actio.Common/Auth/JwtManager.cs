using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Actio.Common.Core;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Actio.Common.Auth
{
    public class JwtManager : IJwtManager
    {
        private readonly JwtOptions options;

        public JwtManager(IOptions<JwtOptions> options)
        {
            this.options = options.Value;
        }

        public JsonWebToken GenerateToken(Guid userId)
        {
            var utcNow = DateTime.Now;
            var expires = utcNow.AddMinutes(options.ExpiryMinutes);
            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.SecretKey)), SecurityAlgorithms.HmacSha256);
            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, userId.ToString()), 
                new Claim(JwtRegisteredClaimNames.Iss, options.Issuer), 
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), 
                new Claim(JwtRegisteredClaimNames.Iat, utcNow.ToTimeStamp().ToString(), ClaimValueTypes.Integer64), 
            };

            var jwt = new JwtSecurityToken(
                issuer: options.Issuer,
                claims: claims,
                notBefore: utcNow,
                expires: expires,
                signingCredentials: signingCredentials
                );

            var token = new JwtSecurityTokenHandler().WriteToken(jwt);
            return new JsonWebToken(token, expires);
        }
    }
}