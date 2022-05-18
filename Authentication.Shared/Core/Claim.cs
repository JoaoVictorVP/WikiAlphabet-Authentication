namespace Authentication.Shared;

public record class Claim(string Name, ClaimLevel Level);

public enum ClaimLevel
{
    ReadOnly,
    WriteOnly,
    WriteAndRead
}