using Authentication.Shared;

namespace Authentication.Server.XIdentity.Contracts;

public interface IServerUser
{
    string Id { get; set; }
    string Username { get; set; }
    string Email { get; set; }
    string Password { get; set; }
    IEnumerable<UserRole> GetAllRoles();
    UserRole? GetRole(string roleName);
    void AddRole(UserRole role);
    void RemoveRole(string roleName);
}
