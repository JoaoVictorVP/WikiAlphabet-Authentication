using Authentication.Server.XIdentity.Contracts;
using Authentication.Shared;
using Authentication.Shared.Contracts;

namespace Authentication.Server.XIdentity.Core.Models;

public class AppUser : IUser
{
    private readonly User _user;

    public AppUser(User user)
    {
        _user = user;
    }

    public string Id { get => _user.Id; set => _user.Id = value; }
    public string Username { get => _user.Username; set => _user.Username = value; }
    public string Email { get => _user.Email; set => _user.Email = value; }
    public string PasswordHash => CryptoUtils.HashPassword(_user.Password, Env.Salt);

    public IEnumerable<UserRole> GetAllRoles() => throw new NotImplementedException();
    public UserRole GetRole(string roleName) => throw new NotImplementedException();
    public void AddRole(UserRole role) => throw new NotImplementedException();
    public void RemoveRole(string roleName) => throw new NotImplementedException();
}
