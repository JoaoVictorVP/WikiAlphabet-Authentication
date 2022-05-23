using Authentication.Server.XIdentity.Contracts.Storage;
using Authentication.Shared.Contracts.Models;

namespace Authentication.Server.XIdentity.Contracts.Factories;

public interface IUserFactory<TServerUser> : IUserStorage<TServerUser> where TServerUser : class, IServerUser
{
}
