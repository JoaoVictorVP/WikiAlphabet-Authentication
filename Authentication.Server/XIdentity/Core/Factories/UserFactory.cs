using Authentication.Server.XIdentity.Contracts.Factories;
using Authentication.Server.XIdentity.Contracts.Repositories;
using Authentication.Shared.Contracts.Models;

namespace Authentication.Server.XIdentity.Core.Factories;

public class UserFactory<TServerUser> : IUserFactory<TServerUser> where TServerUser : class, IServerUser
{
    private readonly IUserRepository<TServerUser> _userRepository;

    public UserFactory(IUserRepository<TServerUser> userRepository)
    {
        _userRepository = userRepository;
    }

    public Task<TServerUser?> GetUser(string serverId, string id) 
        => _userRepository.GetUser(serverId, id);

    public Task<TServerUser?> GetUserByUsername(string serverId, string username) 
        => _userRepository.GetUserByUsername(serverId, username);

    public Task<TServerUser?> GetUserByEmail(string serverId, string email) 
        => _userRepository.GetUserByEmail(serverId, email);

    public Task AddUser(string serverId, TServerUser user) 
        => _userRepository.AddUser(serverId, user);
    
    public Task UpdateUser(string serverId, string id, TServerUser user) 
        => _userRepository.UpdateUser(serverId, id, user);
    
    public Task RemoveUser(string serverId, string id) 
        => _userRepository.RemoveUser(serverId, id);
}
