using Authentication.Shared.Contracts;

namespace Authentication.Server.XIdentity.Contracts;

public interface IBackingSharedUser
{
    IUser User { get; }
}