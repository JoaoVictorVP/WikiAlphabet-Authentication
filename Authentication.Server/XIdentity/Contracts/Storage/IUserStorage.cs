namespace Authentication.Server.XIdentity.Contracts.Storage;

public interface IUserStorage<TServerUser> where TServerUser : class, IServerUser
{
    Task<TServerUser?> GetUser(string serverId, string id);
    Task<TServerUser?> GetUserByUsername(string serverId, string username);
    Task<TServerUser?> GetUserByEmail(string serverId, string email);
    IAsyncEnumerable<TServerUser> GetUsersByRole(string serverId, string roleName);
    Task AddUser(string serverId, TServerUser user);
    Task UpdateUser(string serverId, string id, TServerUser user);
    Task RemoveUser(string serverId, string id);
}
