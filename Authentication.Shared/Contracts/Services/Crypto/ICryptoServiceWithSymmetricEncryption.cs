using Authentication.Shared.Contracts.Models;

namespace Authentication.Shared.Contracts.Services.Crypto;

public interface ICryptoServiceWithSymmetricEncryption<TArgsKey, TArgsIV>
    where TArgsKey : ICryptoArgsWithKey
    where TArgsIV : ICryptoArgsWithIV
{
    byte[] Encrypt(ReadOnlySpan<byte> plaintext, TArgsKey keyArg, TArgsIV? ivArg);
    byte[] Decrypt(ReadOnlySpan<byte> ciphertext, TArgsKey keyArg, TArgsIV? ivArg);
}