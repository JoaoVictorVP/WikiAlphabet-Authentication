namespace Authentication.Shared.Contracts.Models;

public interface ICryptoArgsWithIV : ICryptoArgs
{
    int IVSize { get; }
    byte[] GetIV();
    void GetIV(Span<byte> destIV);
}
