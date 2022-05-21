using Authentication.Shared.Contracts.Services.Crypto;
using System.Security.Cryptography;

namespace Authentication.Shared.Core.Services.Crypto;

public class SHA256CryptoService : ICryptoService, ICryptoServiceWithHashing
{
    public int HashSize => 32;

    public byte[] HashData(ReadOnlySpan<byte> data) => SHA256.HashData(data);
    public void HashData(ReadOnlySpan<byte> data, Span<byte> destination) => SHA256.HashData(data, destination);

    public byte[] HMAC(ReadOnlySpan<byte> key, ReadOnlySpan<byte> data) => HMACSHA256.HashData(key, data);
    public void HMAC(ReadOnlySpan<byte> key, ReadOnlySpan<byte> data, Span<byte> destination) => HMACSHA256.HashData(key, data, destination);
}