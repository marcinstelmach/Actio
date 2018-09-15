using System;

namespace Actio.Common.Auth
{
    public class JsonWebToken
    {
        public JsonWebToken(string token, DateTime expires)
        {
            Token = token;
            Expires = expires;
        }
        public string Token { get; set; }
        public DateTime Expires { get; set; }
    }
}