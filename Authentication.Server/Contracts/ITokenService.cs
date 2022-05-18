using Authentication.Shared;

namespace Authentication.Server.Contracts;

public interface ITokenService
{
    string GenerateToken(string secret, User user, TimeSpan lifetime);
    string GenerateRefreshToken(TimeSpan lifetime);
}
