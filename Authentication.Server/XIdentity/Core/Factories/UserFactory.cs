using Authentication.Server.XIdentity.Contracts;
using Authentication.Server.XIdentity.Contracts.Factories;
using Authentication.Server.XIdentity.Contracts.Repositories;

namespace Authentication.Server.XIdentity.Core.Factories;

public class UserFactory : IUserFactory
{
    private readonly IUserRepository _userRepository;

    public UserFactory(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public Task<IServerUser?> GetUser(string id) => _userRepository.GetUser(id);
    public Task<IServerUser?> GetUserByUsername(string username) => _userRepository.GetUserByUsername(username);
    public Task<IServerUser?> GetUserByEmail(string email) => _userRepository.GetUserByEmail(email);
    public IAsyncEnumerable<IServerUser> GetUsersByRole(string roleName) => _userRepository.GetUsersByRole(roleName);
    public Task AddUser(IServerUser user) => _userRepository.AddUser(user);
    public Task UpdateUser(string id, IServerUser user) => _userRepository.UpdateUser(id, user);
    public Task RemoveUser(string id) => _userRepository.RemoveUser(id);
}
