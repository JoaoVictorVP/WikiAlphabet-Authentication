using Authentication.Shared;

namespace Authentication.Server.Contracts.Services;

public interface ITokenService
{
    string GenerateToken(string secret, User user, TimeSpan lifetime);
    string GenerateRefreshToken(TimeSpan lifetime);
}
