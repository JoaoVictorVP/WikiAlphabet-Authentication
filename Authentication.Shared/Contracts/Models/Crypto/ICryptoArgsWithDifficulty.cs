namespace Authentication.Shared.Contracts.Models;

public interface ICryptoArgsWithDifficulty : ICryptoArgs
{
    void Difficulty(int difficulty);
    int GetDifficulty();
}