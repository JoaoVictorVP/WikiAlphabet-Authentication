namespace Authentication.Shared.Contracts.Services.Crypto;

public interface ICryptoServiceWithHashing : ICryptoService
{
    int HashSize { get; }
    byte[] HashData(ReadOnlySpan<byte> data);
    void HashData(ReadOnlySpan<byte> data, Span<byte> destination);
    
    byte[] HMAC(ReadOnlySpan<byte> key, ReadOnlySpan<byte> data);
    void HMAC(ReadOnlySpan<byte> key, ReadOnlySpan<byte> data, Span<byte> destination);
}