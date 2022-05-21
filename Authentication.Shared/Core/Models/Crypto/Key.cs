using Authentication.Shared.Contracts.Models;

namespace Authentication.Shared.Core.Models.Crypto;

public readonly struct Key : ICryptoArgsWithKey, ICryptoArgsWithGenericGetter
{
    readonly byte[] _key;
    public int KeySize => 0;

    public Key WithKey(byte[] key) => new(key);

    public byte[] GetKey() => _key;
    public void GetKey(Span<byte> destKey) => _key.CopyTo(destKey);

    public T Get<T>(string name) => name.ToString() is "key" or "secret" or "secretKey" ?
        (T)(object)_key : throw new NotImplementedException($"No implementation for {name}");

    public Key(byte[] key) => _key = key;
}
