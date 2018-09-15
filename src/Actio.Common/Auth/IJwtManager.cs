using System;

namespace Actio.Common.Auth
{
    public interface IJwtManager
    {
        JsonWebToken GenerateToken(Guid userId);
    }
}
