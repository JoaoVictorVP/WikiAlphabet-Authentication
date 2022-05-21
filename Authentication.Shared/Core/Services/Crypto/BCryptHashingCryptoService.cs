using Authentication.Shared.Contracts.Services.Crypto;
using Authentication.Shared.Core.Models.Crypto;
using Org.BouncyCastle.Crypto.Generators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Shared.Core.Services.Crypto;

public class BCryptHashingCryptoService : ICryptoService, 
    ICryptoServiceWithPasswordHashing<Salt128, Difficulty>
{
    public void HashPassword(ReadOnlySpan<byte> password, Salt128 salt, Difficulty difficulty, Span<byte> destHashedPassword)
    {
        BCrypt.Generate(password.ToArray(), 
            salt.GetSalt(), 
            difficulty.GetDifficulty())
            .CopyTo(destHashedPassword);
    }

    public byte[] HashPassword(ReadOnlySpan<byte> password, Salt128 salt, Difficulty difficulty)
    {
        return BCrypt.Generate(password.ToArray(),
            salt.GetSalt(),
            difficulty.GetDifficulty());
    }
}
