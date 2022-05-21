using Authentication.Shared.Contracts.Models;

namespace Authentication.Shared.Contracts.Services.Crypto;

public interface ICryptoServiceWithDigitalSignatures<TArgsPrivateKey, TArgsPublicKey>
    where TArgsPrivateKey : ICryptoArgsWithKey
    where TArgsPublicKey : ICryptoArgsWithKey
{
    byte[] Sign(ReadOnlySpan<byte> plaintext, TArgsPrivateKey argsPrivateKey);
    bool Verify(ReadOnlySpan<byte> signature, TArgsPublicKey argsPublicKey);
}