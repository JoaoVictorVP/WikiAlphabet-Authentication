using Authentication.Shared.Contracts.Models;

namespace Authentication.Server.XIdentity.Contracts.Managers;

public interface IServerManager<TServer> where TServer : IServer
{
    Task RegisterAsync(TServer server);
    Task UpdateAsync(TServer server);
    Task RemoveAsync(TServer server);
    Task<TServer?> LoginAsyncWithEmail(string email, string password);
    Task<TServer?> LoginAsyncWithId(string serverId, string password);
    Task<bool> IsValidServerAsync(string serverId);
}