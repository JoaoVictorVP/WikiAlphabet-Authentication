using Authentication.Shared;

namespace Authentication.Server.XIdentity.Contracts.Managers;

public interface IUserManager<TUser> where TUser : IServerUser
{
    string DoHashPassword(string password);
    Task<TUser?> FindByEmailAsync(string email);
    Task<TUser?> FindByUsernameAsync(string username);
    Task<TUser?> GetUserAsync(string userId);
    Task<bool> RegisterAsync(TUser user);
    Task<bool> DeleteAccountAsync(string userId);
    Task<bool> ChangePasswordAsync(string userId, string oldPassword, string newPassword);
    Task<bool> ChangeEmailAsync(string userId, string newEmail, string serverConfirmationCode, string clientConfirmationCode);
    Task<bool> IsValidPasswordAsync(IServerUser user, string passwordOrPasswordHash);
    string GenerateConfirmationCode(string action);
}
