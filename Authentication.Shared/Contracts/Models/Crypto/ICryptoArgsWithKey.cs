namespace Authentication.Shared.Contracts.Models;

public interface ICryptoArgsWithKey : ICryptoArgs
{
    int KeySize { get; }
    byte[] GetKey();
    void GetKey(Span<byte> destKey);
}
