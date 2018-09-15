using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Actio.Common.Auth
{
    public class JwtHandler : IJwtHandler
    {
        private readonly JwtHeader jwtHeader;
        private readonly JwtOptions options;
        private readonly JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        private readonly TokenValidationParameters validationParameters;

        public JwtHandler(IOptions<JwtOptions> options)
        {
            this.options = options.Value;
            var issuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.options.SecretKey));
            var signingCredentials = new SigningCredentials(issuerSigningKey, SecurityAlgorithms.HmacSha256);
            jwtHeader = new JwtHeader(signingCredentials);
            validationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidIssuer = this.options.Issuer,
                IssuerSigningKey = issuerSigningKey
            };
        }

        public JsonWebToken Create(Guid userId)
        {
            var utcNow = DateTime.UtcNow;
            var expiryTime = utcNow.AddMinutes(options.ExpiryMinutes);
            var centuryBegin = new DateTime(1970, 1, 1).ToUniversalTime();
            var exp = (long) new TimeSpan(expiryTime.Ticks - centuryBegin.Ticks).TotalSeconds;
            var now = (long) new TimeSpan(utcNow.Ticks - centuryBegin.Ticks).TotalSeconds;
            var payload = new JwtPayload
            {
                {"sub", userId},
                {"iis", options.Issuer},
                {"iat", now},
                {"exp", exp},
                {"unique_name", userId}
            };

            var jwt = new JwtSecurityToken(jwtHeader, payload);
            var token = tokenHandler.WriteToken(jwt);
            return new JsonWebToken
            {
                Token = token,
                Expires = exp
            };
        }
    }
}