namespace Authentication.Server.XIdentity.Contracts.Managers;

public interface IServerManager<TServer> where TServer : IServer
{
    Task RegisterAsync(TServer server);
    Task UpdateAsync(TServer server);
    Task RemoveAsync(TServer server);
    Task<TServer?> GetServerAsync(string serverId);
    Task<TServer?> GetServerByNameAsync(string serverEmail);
    Task<bool> IsValidServerAsync(string serverId);
}