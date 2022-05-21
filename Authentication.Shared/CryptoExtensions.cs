using System.Text;

namespace Authentication.Shared;

public static class CryptoExtensions
{
    public static string ToBase64(this byte[] bytes) => Convert.ToBase64String(bytes);
    public static string ToBase64(this Span<byte> bytes) => Convert.ToBase64String(bytes);
    public static string ToBase64(this ReadOnlySpan<byte> bytes) => Convert.ToBase64String(bytes);
    public static byte[] FromBase64(this string base64) => Convert.FromBase64String(base64);

    public static string ToHex(this byte[] bytes) => Convert.ToHexString(bytes);
    public static string ToHex(this Span<byte> bytes) => Convert.ToHexString(bytes);
    public static string ToHex(this ReadOnlySpan<byte> bytes) => Convert.ToHexString(bytes);
    public static byte[] FromHex(this ReadOnlySpan<char> hex) => Convert.FromHexString(hex);

    public static byte[] ToUnicodePlaintext(this string text) => Encoding.Unicode.GetBytes(text);
    public static string FromUnicodePlaintext(this ReadOnlySpan<byte> bytes) => Encoding.Unicode.GetString(bytes);

    public static byte[] ToUtf8Plaintext(this string text) => Encoding.UTF8.GetBytes(text);
    public static string FromUtf8Plaintext(this ReadOnlySpan<byte> bytes) => Encoding.UTF8.GetString(bytes);
    
    public static byte[] ToASCIIPlaintext(this string text) => Encoding.ASCII.GetBytes(text);
    public static string FromASCIIPlaintext(this ReadOnlySpan<byte> bytes) => Encoding.ASCII.GetString(bytes);

    public static byte[] ToUtf32Plaintext(this string text) => Encoding.UTF32.GetBytes(text);
    public static string FromUtf32Plaintext(this ReadOnlySpan<byte> bytes) => Encoding.UTF32.GetString(bytes);
}