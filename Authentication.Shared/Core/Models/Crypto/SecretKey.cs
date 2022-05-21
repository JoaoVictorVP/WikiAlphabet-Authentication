using Authentication.Shared.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Shared.Core.Models.Crypto;

public class SecretKey : ICryptoArgsWithKey
{
    byte[] _key = Array.Empty<byte>();
    public int KeySize => 0;

    public void Key(string base64Key) => _key = base64Key.FromBase64();
    public void Key(ReadOnlySpan<byte> key) => _key = key.ToArray();
    public byte[] GetKey() => _key;
    public void GetKey(Span<byte> destKey) => _key.CopyTo(destKey);

    public T Get<T>(string name) => name.ToString() is "key" or "secret" or "secretKey" ?
        (T)(object)_key : throw new NotImplementedException($"No implementation for {name}");

    public void Generic(string name, object value) => throw new NotImplementedException();
}
