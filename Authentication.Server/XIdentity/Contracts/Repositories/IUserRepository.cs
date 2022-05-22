using Authentication.Server.XIdentity.Contracts.Storage;

namespace Authentication.Server.XIdentity.Contracts.Repositories;

public interface IUserRepository<TServerUser> : IUserStorage<TServerUser> where TServerUser : class, IServerUser
{
    
}
