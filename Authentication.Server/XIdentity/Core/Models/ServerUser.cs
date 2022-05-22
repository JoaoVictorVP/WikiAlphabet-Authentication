using Authentication.Server.XIdentity.Contracts;
using Authentication.Shared;
using Authentication.Shared.Contracts;
using System.Runtime.Serialization;

namespace Authentication.Server.XIdentity.Core.Models;

public class ServerUser<TUser> : IServerUser, IBackingSharedUser where TUser : IUser
{
    IUser IBackingSharedUser.User => _user;
    private TUser _user;
    public TUser User { get => _user; set => _user = value; }

    public ServerUser(TUser user)
    {
        _user = user;
    }

    [IgnoreDataMember]
    public string Id { get => _user.Id; set => _user.Id = value; }
    [IgnoreDataMember]
    public string Username { get => _user.Username; set => _user.Username = value; }
    [IgnoreDataMember]
    public string Email { get => _user.Email; set => _user.Email = value; }
    [IgnoreDataMember]
    public string Password { get => _user.Password; set => _user.Password = value; }

    public IEnumerable<UserRole> GetAllRoles() => _user.UserRoles;
    public UserRole? GetRole(string roleName) => _user.UserRoles.Find(role => role.RoleName == roleName);
    public void AddRole(UserRole role) => _user.UserRoles.Add(role);
    public void RemoveRole(string roleName) => _user.UserRoles.RemoveAll(role => role.RoleName == roleName);
}
