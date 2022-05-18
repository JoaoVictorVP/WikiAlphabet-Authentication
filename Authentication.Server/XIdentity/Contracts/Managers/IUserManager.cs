using Authentication.Shared;

namespace Authentication.Server.XIdentity.Contracts.Managers;

public interface IUserManager<TUser> where TUser : IUser
{
    Task<bool> Register(TUser user);
    Task<bool> DeleteAccount(string userId);
    Task<bool> ChangePassword(string userId, string oldPassword, string newPassword);
    Task<bool> ChangeEmail(string userId, string newEmail, string serverConfirmationCode, string clientConfirmationCode);
    Task<bool> IsValidPassword(string userId, string passwordHash);
    string GenerateConfirmationCode(string action);
}
