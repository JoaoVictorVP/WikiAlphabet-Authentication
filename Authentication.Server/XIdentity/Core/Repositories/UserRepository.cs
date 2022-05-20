using Authentication.Server.XIdentity.Contracts;
using Authentication.Server.XIdentity.Contracts.Repositories;

namespace Authentication.Server.XIdentity.Core.Repositories;

public class UserRepository : IUserRepository
{
    readonly Dictionary<string, IServerUser> users = new(32);

    public Task<IServerUser?> GetUser(string id)
    {
        users.TryGetValue(id, out var user);
        return Task.FromResult(user);
    }

    public Task<IServerUser?> GetUserByUsername(string username)
    {
        return Task.FromResult(users.Values.FirstOrDefault(u => u.Username == username));
    }

    public Task<IServerUser?> GetUserByEmail(string email)
    {
        return Task.FromResult(users.Values.FirstOrDefault(u => u.Email == email));
    }

    public async IAsyncEnumerable<IServerUser> GetUsersByRole(string roleName)
    {
        var withEmail = users.Where(x => x.Value.GetRole(roleName) is not null);
        foreach (var (_, user) in withEmail)
            yield return user;
    }

    public Task AddUser(IServerUser user)
    {
        users[user.Id] = user;
        return Task.CompletedTask;
    }

    public Task UpdateUser(string id, IServerUser user)
    {
        users[id] = user;
        return Task.CompletedTask;
    }

    public Task RemoveUser(string id)
    {
        users.Remove(id);
        return Task.CompletedTask;
    }
}
