using Authentication.Shared;
using Authentication.Shared.Contracts;

namespace Authentication.Server.XIdentity.Contracts;

public interface IUser
{
    string Id { get; set; }
    string Username { get; set; }
    string Email { get; set; }
    string PasswordHash { get; }
    IEnumerable<UserRole> GetAllRoles();
    UserRole? GetRole(string roleName);
    void AddRole(UserRole role);
    void RemoveRole(string roleName);
}