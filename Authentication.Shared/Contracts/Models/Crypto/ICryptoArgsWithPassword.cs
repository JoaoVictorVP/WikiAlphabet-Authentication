namespace Authentication.Shared.Contracts.Models;

public interface ICryptoArgsWithPassword : ICryptoArgs
{
    string GetPassword();
}
