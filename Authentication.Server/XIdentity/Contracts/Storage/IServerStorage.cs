using Authentication.Shared.Contracts.Models;

namespace Authentication.Server.XIdentity.Contracts.Storage;

public interface IServerStorage<TServer> where TServer : IServer
{
    Task AddServerAsync(TServer server);
    Task UpdateServerAsync(TServer server);
    Task RemoveServerAsync(TServer server);

    Task<TServer?> GetServerAsync(string id);
    Task<TServer?> GetServerByEmailAsync(string email);
}