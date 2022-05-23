using Authentication.Shared;

namespace Authentication.Shared.Contracts.Models;

public interface IServerUser
{
    string Id { get; set; }
    string FullName { get; set; }
    string Username { get; set; }
    string Email { get; set; }
    string Password { get; set; }
    DateTime CreatedAt { get; set; }
    DateTime UpdatedAt { get; set; }
    bool Verified { get; set; }

    void AddServer(string serverId);
    void RemoveServer(string serverId);
    bool InServer(string serverId);
}
