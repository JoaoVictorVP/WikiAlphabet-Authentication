namespace Authentication.Shared.Contracts.Models;

public interface ICryptoArgsWithIterations : ICryptoArgs
{
    int GetIterations();
}
