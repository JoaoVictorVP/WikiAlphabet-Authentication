namespace Authentication.Shared.Contracts.Models;

public interface ICryptoArgsWithIV : ICryptoArgs
{
    int IVSize { get; }
    void IV(string base64IV);
    void IV(ReadOnlySpan<byte> iv);

    byte[] GetIV();
    void GetIV(Span<byte> destIV);
}
