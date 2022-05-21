using Authentication.Shared.Contracts.Models;

namespace Authentication.Shared.Contracts.Services.Crypto;

public interface ICryptoServiceWithAsymmetricEncryption<TArgsPrivateKey, TArgsPublicKey>
    where TArgsPrivateKey : ICryptoArgsWithKey
    where TArgsPublicKey : ICryptoArgsWithKey
{
    void Encrypt(ReadOnlySpan<byte> plaintext, TArgsPublicKey argsPublicKey);
    void Decrypt(ReadOnlySpan<byte> ciphertext, TArgsPrivateKey argsPrivateKey);
}