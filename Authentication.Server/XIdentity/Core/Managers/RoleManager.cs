using Authentication.Server.XIdentity.Contracts;
using Authentication.Shared;

namespace Authentication.Server.XIdentity.Core.Managers;

public class RoleManager : IRoleManager<Role>
{
    readonly List<Role> roles = new(32);

    public void AddRole(Role role)
    {
        roles.Add(role);
    }

    public void RemoveRole(Role role)
    {
        roles.Remove(role);
    }

    public Role? GetRole(string name)
    {
        return roles.Find(role => role.Name == name);
    }
}
