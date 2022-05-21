using Authentication.Shared.Contracts.Models;

namespace Authentication.Shared.Core.Models.Crypto;

public readonly record struct Difficulty(int Value) : ICryptoArgsWithDifficulty
{
    public int GetDifficulty() => Value;
}