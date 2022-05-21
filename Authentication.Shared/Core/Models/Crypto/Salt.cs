using Authentication.Shared.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Shared.Core.Models.Crypto;

public readonly struct Salt : ICryptoArgsWithSalt
{
    readonly byte[] _salt;

    public Salt(byte[] salt) => _salt = salt;
    public Salt(ReadOnlySpan<byte> salt) => _salt = salt.ToArray();

    public int SaltSize => 0;

    public byte[] GetSalt() => _salt;
    public void GetSalt(Span<byte> destSalt) => _salt.CopyTo(destSalt);
}
