using Authentication.Server.XIdentity.Contracts.Factories;
using Authentication.Server.XIdentity.Contracts.Repositories;
using Authentication.Shared.Contracts.Models;

namespace Authentication.Server.XIdentity.Core.Factories;

public class ServerFactory<TServer> : IServerFactory<TServer> where TServer : IServer
{
    private readonly IServerRepository<TServer> _serverRepository;

    public ServerFactory(IServerRepository<TServer> serverRepository)
    {
        _serverRepository = serverRepository;
    }

    public async Task AddServerAsync(TServer server)
    {
        await _serverRepository.AddServerAsync(server);
    }

    public async Task UpdateServerAsync(TServer server)
    {
        await _serverRepository.UpdateServerAsync(server);
    }

    public async Task RemoveServerAsync(TServer server)
    {
        await _serverRepository.RemoveServerAsync(server);
    }

    public async Task<TServer?> GetServerAsync(string id)
    {
        return await _serverRepository.GetServerAsync(id);
    }

    public async Task<TServer?> GetServerByEmailAsync(string email)
    {
        return await _serverRepository.GetServerByEmailAsync(email);
    }
}