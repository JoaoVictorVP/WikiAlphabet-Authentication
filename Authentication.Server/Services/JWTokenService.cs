using Authentication.Server.Contracts.Services;
using Authentication.Shared;

namespace Authentication.Server.Services;

public class JWTokenService : ITokenService
{
    public string GenerateToken(string secret, User user, TimeSpan lifetime)
    {
        throw new NotImplementedException();
    }

    public string GenerateRefreshToken(TimeSpan lifetime)
    {
        throw new NotImplementedException();
    }
}
