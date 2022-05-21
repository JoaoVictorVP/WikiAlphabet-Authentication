using Authentication.Shared.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Shared.Core.Models.Crypto;

public class SecretKey : ICryptoArgsWithKey
{
    public int KeySize => 0;

    public void Key(string base64Key) => throw new NotImplementedException();
    public void Key(ReadOnlySpan<byte> key) => throw new NotImplementedException();
    public byte[] GetKey() => throw new NotImplementedException();
    public void GetKey(Span<byte> destKey) => throw new NotImplementedException();
    public void Generic(string name, object value) => throw new NotImplementedException();
    public T Get<T>(string name) => throw new NotImplementedException();
}
