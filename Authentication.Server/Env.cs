namespace Authentication.Server;

public static class Env
{
    public static readonly string ApplicationName = "weProtect";
    public static readonly string ApplicationId = "wisocialmedia";
    public static readonly TimeSpan TokenLifetime = TimeSpan.Parse(Environment.GetEnvironmentVariable("WIKI_TOKEN_LIFETIME") ?? "8:00:00");
    public static readonly DateTime TokenExpirationDate = DateTime.UtcNow.Add(TokenLifetime);
    public static readonly byte[] Secret = Convert.FromBase64String(Environment.GetEnvironmentVariable("WIKI_AUTH_SECRET")
        ?? throw new NotImplementedException("Need to define an environment variable called WIKI_AUTH_SECRET"));

    public static readonly byte[] Salt = Convert.FromBase64String(Environment.GetEnvironmentVariable("WIKI_AUTH_SALT")
        ?? throw new NotImplementedException("Need to define an environment variable called WIKI_AUTH_SALT"));
    
    public static readonly int BCryptWorkFactor = int.Parse(Environment.GetEnvironmentVariable("WIKI_AUTH_BCRYPT_WORK_FACTOR")
        ?? throw new NotImplementedException("Need to define an environment variable called WIKI_AUTH_BCRYPT_WORK_FACTOR (default value: 10)"));
}
