using Authentication.Server.XIdentity.Contracts;
using Authentication.Server.XIdentity.Contracts.Repositories;

namespace Authentication.Server.XIdentity.Core.Repositories;

public class UserRepository : IUserRepository
{
    Dictionary<string, IUser> users = new(32);

    public Task<IUser?> GetUser(string id)
    {
        users.TryGetValue(id, out var user);
        return Task.FromResult(user);
    }

    public Task<IUser?> GetUserByUsername(string username)
    {
        return Task.FromResult(users.Values.FirstOrDefault(u => u.Username == username));
    }

    public Task<IUser?> GetUserByEmail(string email)
    {
        return Task.FromResult(users.Values.FirstOrDefault(u => u.Email == email));
    }

    public async IAsyncEnumerable<IUser> GetUsersByRole(string roleName)
    {
        var withEmail = users.Where(x => x.Value.GetRole(roleName) is not null);
        foreach (var (_, user) in withEmail)
            yield return user;
    }

    public Task AddUser(IUser user)
    {
        users[user.Id] = user;
        return Task.CompletedTask;
    }

    public Task RemoveUser(string id)
    {
        users.Remove(id);
        return Task.CompletedTask;
    }
}
