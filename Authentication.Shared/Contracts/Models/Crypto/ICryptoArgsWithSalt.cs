namespace Authentication.Shared.Contracts.Models;

public interface ICryptoArgsWithSalt : ICryptoArgs
{
    int SaltSize { get; }
    byte[] GetSalt();
    void GetSalt(Span<byte> destSalt);
}
