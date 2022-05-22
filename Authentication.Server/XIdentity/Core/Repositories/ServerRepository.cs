using Authentication.Server.Databases.Contracts;
using Authentication.Server.XIdentity.Contracts.Repositories;
using Authentication.Shared.Contracts.Models;
using LiteDB;

namespace Authentication.Server.XIdentity.Core.Repositories;

public class ServerRepository<TServer> : IServerRepository<TServer> where TServer : IServer
{
    private readonly IDatabaseFactory<LiteDatabase> _databaseFactory;
    private readonly ILiteCollection<TServer> _servers;

    public ServerRepository(IDatabaseFactory<LiteDatabase> databaseFactory)
    {
        _databaseFactory = databaseFactory;
        _servers = _databaseFactory.Get().GetCollection<TServer>("Servers");
    }

    public Task AddServerAsync(TServer server)
    {
        return Task.Run(() => _servers.Insert(server));
    }

    public Task UpdateServerAsync(TServer server)
    {
        return Task.Run(() => _servers.Update(server));
    }

    public Task RemoveServerAsync(TServer server)
    {
        return Task.Run(() => _servers.Delete(server.Id));
    }

    public Task<TServer?> GetServerAsync(string id)
    {
        return Task.Run<TServer?>(() => _servers.FindOne(server => server.Id == id));
    }

    public Task<TServer?> GetServerByEmailAsync(string email)
    {
        return Task.Run<TServer?>(() => _servers.FindOne(server => server.Email == email));
    }
}