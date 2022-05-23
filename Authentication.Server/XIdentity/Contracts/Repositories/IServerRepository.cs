using Authentication.Server.XIdentity.Contracts.Storage;
using Authentication.Shared.Contracts.Models;

namespace Authentication.Server.XIdentity.Contracts.Repositories;

public interface IServerRepository<TServer> : IServerStorage<TServer> where TServer : IServer
{
    
}