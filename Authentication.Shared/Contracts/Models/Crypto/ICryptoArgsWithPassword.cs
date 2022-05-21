namespace Authentication.Shared.Contracts.Models;

public interface ICryptoArgsWithPassword : ICryptoArgs
{
    void Password(string password);
    string GetPassword();
}
