using Authentication.Shared;

namespace Authentication.Server.XIdentity.Contracts.Managers;

public interface IUserManager<TUser> where TUser : IUser
{
    bool Register(IUser user);
    bool DeleteAccount(string userId);
    bool ChangePassword(string userId, string oldPassword, string newPassword);
    bool ChangeEmail(string userId, string newEmail);
    bool IsValidPassword(string userId, string passwordHash);
}
