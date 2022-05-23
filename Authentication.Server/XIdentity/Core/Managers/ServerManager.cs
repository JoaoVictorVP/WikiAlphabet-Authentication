using Authentication.Server.XIdentity.Contracts.Factories;
using Authentication.Server.XIdentity.Contracts.Managers;
using Authentication.Shared;
using Authentication.Shared.Contracts.Models;
using Authentication.Shared.Contracts.Services.Crypto;
using Authentication.Shared.Core.Models.Crypto;

namespace Authentication.Server.XIdentity.Core.Managers;

public class ServerManager<TServer> : IServerManager<TServer> where TServer : IServer
{
    private readonly IServerFactory<TServer> _serverFactory;
    private readonly ICryptoServiceWithPasswordHashing<Salt128, Difficulty> _passwordHashingService;

    public ServerManager(IServerFactory<TServer> serverFactory, ICryptoServiceWithPasswordHashing<Salt128, Difficulty> passwordHashingService)
    {
        _serverFactory = serverFactory;
        _passwordHashingService = passwordHashingService;
    }

    string DoHashPassword(string password)
    {
        return _passwordHashingService.HashPassword(
            password.ToUnicodePlaintext(),
            new Salt128(Env.Salt),
            new Difficulty(Env.BCryptWorkFactor)).ToBase64();
    }

    public async Task RegisterAsync(TServer server)
    {
        server.Password = DoHashPassword(server.Password);
        
        await _serverFactory.AddServerAsync(server);
    }

    public async Task UpdateAsync(TServer server)
    {
        await _serverFactory.UpdateServerAsync(server);
    }

    public async Task RemoveAsync(TServer server)
    {
        await _serverFactory.RemoveServerAsync(server);
    }

    public async Task<TServer?> LoginAsyncWithEmail(string email, string password)
    {
        password = DoHashPassword(password);
        var server = await _serverFactory.GetServerByEmailAsync(email);

        return server is not null && server.Password == password
            ? server
            : default;
    }

    public async Task<TServer?> LoginAsyncWithId(string serverId, string password)
    {
        password = DoHashPassword(password);
        var server = await _serverFactory.GetServerAsync(serverId);

        return server is not null && server.Password == password
            ? server
            : default;
    }

    public async Task<bool> IsValidServerAsync(string serverId)
    {
        return await _serverFactory.GetServerAsync(serverId) is not null;
    }
}