using Authentication.Shared.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Shared.Core.Models.Crypto;

public readonly struct SecretKey : ICryptoArgsWithKey, ICryptoArgsWithGenericGetter
{
    readonly byte[] _key;
    public int KeySize => 0;

    public byte[] GetKey() => _key;
    public void GetKey(Span<byte> destKey) => _key.CopyTo(destKey);

    public T Get<T>(string name) => name.ToString() is "key" or "secret" or "secretKey" ?
        (T)(object)_key : throw new NotImplementedException($"No implementation for {name}");

    public SecretKey(byte[] key) => _key = key;
}
