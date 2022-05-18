using Authentication.Server.XIdentity.Contracts;
using Authentication.Shared;

namespace Authentication.Server.Contracts.Services;

public interface ITokenService
{
    string GenerateToken(string secret, IUser user, TimeSpan lifetime);
    string GenerateRefreshToken(string fromToken, TimeSpan lifetime);
    bool CanRefresh(string fromToken, string refreshToken);
}
