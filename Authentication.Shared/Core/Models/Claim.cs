using Authentication.Shared.Contracts;

namespace Authentication.Shared;

public record class Claim(string Name, ClaimLevel Level) : IClaim;