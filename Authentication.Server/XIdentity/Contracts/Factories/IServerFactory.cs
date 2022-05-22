using Authentication.Server.XIdentity.Contracts.Storage;

namespace Authentication.Server.XIdentity.Contracts.Factories;

public interface IServerFactory<TServer> : IServerStorage<TServer> where TServer : IServer
{
}