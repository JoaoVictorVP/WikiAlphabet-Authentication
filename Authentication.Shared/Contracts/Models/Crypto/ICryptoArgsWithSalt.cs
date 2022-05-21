namespace Authentication.Shared.Contracts.Models;

public interface ICryptoArgsWithSalt : ICryptoArgs
{
    int SaltSize { get; }
    void Salt(string base64Salt);
    void Salt(ReadOnlySpan<byte> salt);

    byte[] GetSalt();
    void GetSalt(Span<byte> destSalt);
}
