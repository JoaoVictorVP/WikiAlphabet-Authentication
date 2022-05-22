namespace Authentication.Shared.Contracts.Models;

public interface IServer
{
    string Id { get; set; }
    string Email { get; set; }
    string Password { get; set; }

    // Endpoints
    string OnUserRegisteredEndpoint { get; set; }
    string OnUserLogggedInEndpoint { get; set; }
    string GetUserTokenEndpoint { get; set; }
    string OnUserDeletedAccountEndpoint { get; set; }
}