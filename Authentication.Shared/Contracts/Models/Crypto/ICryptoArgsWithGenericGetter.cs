namespace Authentication.Shared.Contracts.Models;

public interface ICryptoArgsWithGenericGetter
{
    T Get<T>(string name);
}