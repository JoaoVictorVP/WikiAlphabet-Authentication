using Authentication.Server.Databases.Contracts;
using Authentication.Server.XIdentity.Contracts;
using Authentication.Server.XIdentity.Contracts.Managers;
using Authentication.Server.XIdentity.Contracts.Repositories;
using Authentication.Shared.Contracts.Models;
using LiteDB;

namespace Authentication.Server.XIdentity.Core.Repositories;

public class UserRepository<TServerUser> : IUserRepository<TServerUser> where TServerUser : class, IServerUser
{
    private readonly IDatabaseFactory<LiteDatabase> _databaseFactory;
    private readonly IServerManager<IServer> _serverManager;
    private readonly ILiteDatabase _db;

    public UserRepository(IDatabaseFactory<LiteDatabase> databaseFactory, IServerManager<defServer> serverManager)
    {
        _databaseFactory = databaseFactory;
        _serverManager = serverManager;
        _db = _databaseFactory.Get();
    }

    public async Task<TServerUser?> GetUser(string serverId, string id)
    {
        if (await _serverManager.IsValidServerAsync(serverId) is false)
            return null;
        var users = _db.GetCollection<TServerUser>(serverId);
        var user = users.FindById(id);
        return user;
    }

    public async Task<TServerUser?> GetUserByUsername(string serverId, string username)
    {
        if (await _serverManager.IsValidServerAsync(serverId) is false)
            return null;
        var users = _db.GetCollection<TServerUser>(serverId);
        return users.FindOne(u => u.Username == username);
    }

    public async Task<TServerUser?> GetUserByEmail(string serverId, string email)
    {
        if (await _serverManager.IsValidServerAsync(serverId) is false)
            return null;
        var users = _db.GetCollection<TServerUser>(serverId);
        return users.FindOne(u => u.Email == email);
    }

    public async IAsyncEnumerable<TServerUser> GetUsersByRole(string serverId, string roleName)
    {
        if (await _serverManager.IsValidServerAsync(serverId) is false)
            yield break;

        var users = _db.GetCollection<TServerUser>(serverId);
        var withRole = users.Find((x => x.GetRole(roleName) != null));

        foreach (var user in withRole)
            yield return user;
    }

    public async Task AddUser(string serverId, TServerUser user)
    {
        if (await _serverManager.IsValidServerAsync(serverId) is false)
            return;
        var users = _db.GetCollection<TServerUser>(serverId);
        users.Insert(user);
    }

    public async Task UpdateUser(string serverId, string id, TServerUser user)
    {
        if (await _serverManager.IsValidServerAsync(serverId) is false)
            return;
        var users = _db.GetCollection<TServerUser>(serverId);
        users.Update(user);
    }

    public async Task RemoveUser(string serverId, string id)
    {
        if (await _serverManager.IsValidServerAsync(serverId) is false)
            return;
        var users = _db.GetCollection<TServerUser>(serverId);
        users.Delete(id);
    }
}
