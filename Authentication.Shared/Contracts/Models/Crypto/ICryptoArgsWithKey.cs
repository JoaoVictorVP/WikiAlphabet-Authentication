namespace Authentication.Shared.Contracts.Models;

public interface ICryptoArgsWithKey : ICryptoArgs
{
    int KeySize { get; }
    void Key(string base64Key);
    void Key(ReadOnlySpan<byte> key);

    byte[] GetKey();
    void GetKey(Span<byte> destKey);
}
