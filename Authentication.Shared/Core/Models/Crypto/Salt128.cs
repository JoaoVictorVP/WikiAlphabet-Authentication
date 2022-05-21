using Authentication.Shared.Contracts.Models;

namespace Authentication.Shared.Core.Models.Crypto;

public readonly struct Salt128 : ICryptoArgsWithSalt
{
    const int ConstSaltSize = 128 / 8;
    readonly byte[] _salt;

    public Salt128(byte[] salt)
    {
        if (salt.Length != ConstSaltSize)
            throw new ArgumentException($"Salt must be {ConstSaltSize} bytes long");
        _salt = salt;
    }

    public Salt128(ReadOnlySpan<byte> salt)
    {
        if(salt.Length != ConstSaltSize)
            throw new ArgumentException($"Salt must be {ConstSaltSize} bytes long");
        _salt = salt.ToArray();
    }

    public int SaltSize => ConstSaltSize;

    public byte[] GetSalt() => _salt;
    public void GetSalt(Span<byte> destSalt)
    {
        _salt.CopyTo(destSalt);
    }
}