using Authentication.Shared;
using Authentication.Shared.Contracts.Models;

namespace Authentication.Server.Contracts.Services;

public interface ITokenService
{
    string GenerateToken(byte[] secret, IServerUser user, TimeSpan lifetime);
    string GenerateRefreshToken(byte[] secret, string fromToken, TimeSpan lifetime);
    bool CanRefresh(string fromToken, string refreshToken);
}
