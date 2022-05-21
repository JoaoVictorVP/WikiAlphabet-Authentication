namespace Authentication.Shared.Contracts.Models;

public interface ICryptoArgsWithDifficulty : ICryptoArgs
{
    int GetDifficulty();
}