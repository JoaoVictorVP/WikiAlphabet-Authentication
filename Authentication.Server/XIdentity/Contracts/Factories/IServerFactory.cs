using Authentication.Server.XIdentity.Contracts.Storage;
using Authentication.Shared.Contracts.Models;

namespace Authentication.Server.XIdentity.Contracts.Factories;

public interface IServerFactory<TServer> : IServerStorage<TServer> where TServer : IServer
{
}