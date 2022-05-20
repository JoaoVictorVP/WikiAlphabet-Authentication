namespace Authentication.Server.XIdentity.Contracts.Storage;

public interface IUserStorage
{
    Task<IServerUser?> GetUser(string id);
    Task<IServerUser?> GetUserByUsername(string username);
    Task<IServerUser?> GetUserByEmail(string email);
    IAsyncEnumerable<IServerUser> GetUsersByRole(string roleName);
    Task AddUser(IServerUser user);
    Task UpdateUser(string id, IServerUser user);
    Task RemoveUser(string id);
}
