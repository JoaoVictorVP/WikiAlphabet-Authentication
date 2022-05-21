namespace Authentication.Shared;

public static class CryptoExtensions
{
    public static string ToBase64(this ReadOnlySpan<byte> bytes) => Convert.ToBase64String(bytes);
    public static byte[] FromBase64(this string base64) => Convert.FromBase64String(base64);

    public static string ToHex(this ReadOnlySpan<byte> bytes) => Convert.ToHexString(bytes);
    public static byte[] FromHex(this ReadOnlySpan<char> hex) => Convert.FromHexString(hex);
}