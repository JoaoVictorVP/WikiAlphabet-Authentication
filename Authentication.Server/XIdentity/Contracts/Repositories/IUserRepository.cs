using Authentication.Server.XIdentity.Contracts.Storage;
using Authentication.Shared.Contracts.Models;

namespace Authentication.Server.XIdentity.Contracts.Repositories;

public interface IUserRepository<TServerUser> : IUserStorage<TServerUser> where TServerUser : class, IServerUser
{
    
}
