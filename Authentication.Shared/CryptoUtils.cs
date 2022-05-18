using Org.BouncyCastle.Crypto.Generators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Shared;

public static class CryptoUtils
{
    public static string HashPassword(string password, byte[] salt, int cost = 6)
    {
        return Convert.ToBase64String(BCrypt.Generate(Encoding.Unicode.GetBytes(password), salt, cost));
    }

    public static string HashWithMAC(byte[] salt, byte[] actionBytes)
    {
        Span<byte> hash = stackalloc byte[32];
        HMACSHA256.HashData(salt, actionBytes, hash);
        return Convert.ToBase64String(hash);
    }
}
