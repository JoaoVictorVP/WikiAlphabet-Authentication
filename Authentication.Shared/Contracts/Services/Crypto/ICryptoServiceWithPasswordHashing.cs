using Authentication.Shared.Contracts.Models;

namespace Authentication.Shared.Contracts.Services.Crypto;

public interface ICryptoServiceWithPasswordHashing<TArgsSalt, TArgsDifficulty> : ICryptoService
    where TArgsSalt : ICryptoArgsWithSalt
    where TArgsDifficulty : ICryptoArgsWithDifficulty
{
    void HashPassword(ReadOnlySpan<byte> password, TArgsSalt salt, TArgsDifficulty difficulty, Span<byte> destHashedPassword);
    byte[] HashPassword(ReadOnlySpan<byte> password, TArgsSalt salt, TArgsDifficulty difficulty);
}