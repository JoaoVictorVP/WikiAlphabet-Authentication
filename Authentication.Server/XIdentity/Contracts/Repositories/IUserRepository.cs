namespace Authentication.Server.XIdentity.Contracts.Repositories;

public interface IUserRepository
{
    IUser GetUser(IUser user);
    IUser GetUserByUsername(string username);
    IUser GetUserByEmail(string email);
    IAsyncEnumerable<IUser> GetUsersByRole(string roleName);
    void AddUser(IUser user);
    void RemoveUser(string id);
}
