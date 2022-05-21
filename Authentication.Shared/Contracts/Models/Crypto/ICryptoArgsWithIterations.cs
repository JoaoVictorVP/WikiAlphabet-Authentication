namespace Authentication.Shared.Contracts.Models;

public interface ICryptoArgsWithIterations : ICryptoArgs
{
    void Iterations(int iterations);
    int GetIterations();
}
