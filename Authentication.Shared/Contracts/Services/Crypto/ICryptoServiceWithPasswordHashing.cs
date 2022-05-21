using Authentication.Shared.Contracts.Models;

namespace Authentication.Shared.Contracts.Services.Crypto;

public interface ICryptoServiceWithPasswordHashing<TArgsSalt, TArgsDifficulty> : ICryptoService
    where TArgsSalt : ICryptoArgsWithSalt
    where TArgsDifficulty : ICryptoArgsWithDifficulty
{
    string HashPassword(string password, TArgsSalt argsSalt, TArgsDifficulty argsDifficulty);
}