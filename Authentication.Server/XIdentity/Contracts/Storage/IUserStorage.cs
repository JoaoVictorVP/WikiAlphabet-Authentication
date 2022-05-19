namespace Authentication.Server.XIdentity.Contracts.Storage;

public interface IUserStorage
{
    Task<IUser?> GetUser(string id);
    Task<IUser?> GetUserByUsername(string username);
    Task<IUser?> GetUserByEmail(string email);
    IAsyncEnumerable<IUser> GetUsersByRole(string roleName);
    Task AddUser(IUser user);
    Task UpdateUser(string id, IUser user);
    Task RemoveUser(string id);
}
