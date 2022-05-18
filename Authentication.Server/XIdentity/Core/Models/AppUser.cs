using Authentication.Server.XIdentity.Contracts;
using Authentication.Shared;
using Authentication.Shared.Contracts;
using System.Runtime.Serialization;

namespace Authentication.Server.XIdentity.Core.Models;

public class AppUser : IUser
{
    private User _user;
    public User User { get => _user; set => _user = value; }

    public AppUser(User user)
    {
        _user = user;
    }

    [IgnoreDataMember]
    public string Id { get => _user.Id; set => _user.Id = value; }
    [IgnoreDataMember]
    public string Username { get => _user.Username; set => _user.Username = value; }
    [IgnoreDataMember]
    public string Email { get => _user.Email; set => _user.Email = value; }
    public string PasswordHash => CryptoUtils.HashPassword(_user.Password, Env.Salt);

    public IEnumerable<UserRole> GetAllRoles() => _user.UserRoles;
    public UserRole? GetRole(string roleName) => _user.UserRoles.Find(role => role.RoleName == roleName);
    public void AddRole(UserRole role) => _user.UserRoles.Add(role);
    public void RemoveRole(string roleName) => _user.UserRoles.RemoveAll(role => role.RoleName == roleName);
}
