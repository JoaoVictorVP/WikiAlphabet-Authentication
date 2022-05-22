namespace Authentication.Server.XIdentity.Contracts;

public interface IServer
{
    string Id { get; set; }
    string Email { get; set; }
    string Password { get; set; }
}