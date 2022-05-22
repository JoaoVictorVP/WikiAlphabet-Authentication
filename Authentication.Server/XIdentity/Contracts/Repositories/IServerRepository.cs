using Authentication.Server.XIdentity.Contracts.Storage;

namespace Authentication.Server.XIdentity.Contracts.Repositories;

public interface IServerRepository<TServer> : IServerStorage<TServer> where TServer : IServer
{
    
}