using Authentication.Shared.Contracts;

namespace Authentication.Server.XIdentity.Contracts;

public interface IRoleManager<TRole> where TRole : IRole
{
    void AddRole(TRole role);
    void RemoveRole(TRole role);
    TRole GetRole(string name);
}
