using Authentication.Server.Contracts.Services;
using Authentication.Server.XIdentity.Contracts;
using Authentication.Shared;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using SecClaim = System.Security.Claims.Claim;

namespace Authentication.Server.Services;

public class JWTokenService : ITokenService
{
    const string InvalidToken = "INVALID_TOKEN";
    const string ClaimTypeID = "ID";

    static string GenerateTokenID()
    {
        var random = RandomNumberGenerator.GetBytes(32);
        return Convert.ToBase64String(random);
    }

    static IEnumerable<SecClaim> GetClaimsFromUser(IUser user)
    {
        var list = new List<SecClaim>(32);

        list.Add(new SecClaim(ClaimTypeID, GenerateTokenID()));

        list.Add(new SecClaim(ClaimTypes.Name, user.Username));

        foreach (var role in user.GetAllRoles())
            list.Add(new SecClaim(ClaimTypes.Role, role.RoleName));

        return list;
    }

    public string GenerateToken(byte[] secret, IUser user, TimeSpan lifetime)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = new SymmetricSecurityKey(secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Expires = DateTime.UtcNow.Add(lifetime),
            Subject = new ClaimsIdentity(GetClaimsFromUser(user)),
            SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }

    public string GenerateRefreshToken(byte[] secret, string fromToken, TimeSpan lifetime)
    {
        var token = new JwtSecurityToken(fromToken);
        var id = token.Claims.FirstOrDefault(claim => claim.Type == ClaimTypeID);
        if (id is null)
            return InvalidToken;

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = new SymmetricSecurityKey(secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Expires = DateTime.UtcNow.Add(lifetime),
            Subject = new ClaimsIdentity(new[]
            {
                new SecClaim(ClaimTypeID, id.Value)
            }),
            SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
        };
        var refreshToken = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(refreshToken);
    }

    public bool CanRefresh(string fromToken, string refreshToken)
    {
        var token = new JwtSecurityToken(fromToken);
        var id = token.Claims.FirstOrDefault(claim => claim.Type == ClaimTypeID);
        if (id is null)
            return false;

        var decodedRefreshToken = new JwtSecurityToken(refreshToken);
        var tokenIdInRefresh = decodedRefreshToken.Claims.FirstOrDefault(claim => claim.Type == ClaimTypeID);
        if (tokenIdInRefresh is null)
            return false;

        return id.Value == tokenIdInRefresh.Value;
    }
}
