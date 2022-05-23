using Authentication.Shared;
using Authentication.Shared.Contracts;
using Authentication.Shared.Contracts.Models;
using System.Runtime.Serialization;

namespace Authentication.Shared.Core.Models;

public class ServerUser : IServerUser
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string FullName { get; set; } = "";
    public string Username { get; set; } = "";
    public string Email { get; set; } = "";
    public string Password { get; set; } = "";
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool Verified { get; set; }
    public List<string> Servers { get; set; } = new (32);

    public void AddServer(string serverId) => Servers.Add(serverId);
    public void RemoveServer(string serverId) => Servers.Remove(serverId);
    public bool InServer(string serverId) => Servers.Contains(serverId);
}
