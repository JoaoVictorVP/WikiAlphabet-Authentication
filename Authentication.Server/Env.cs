namespace Authentication.Server;

public static class Env
{
    public static readonly byte[] Secret = Convert.FromBase64String(Environment.GetEnvironmentVariable("WIKI_AUTH_SECRET")
        ?? throw new NotImplementedException("Need to define an environment variable called WIKI_AUTH_SECRET"));

    public static readonly byte[] Salt = Convert.FromBase64String(Environment.GetEnvironmentVariable("WIKI_AUTH_SALT")
        ?? throw new NotImplementedException("Need to define an environment variable called WIKI_AUTH_SALT"));
}
