using Authentication.Shared;
using Authentication.Shared.Contracts.Models;

namespace Authentication.Server.XIdentity.Contracts.Managers;

public interface IUserManager<TUser> where TUser : IServerUser
{
    string DoHashPassword(string serverId, string password);
    Task<TUser?> FindByEmailAsync(string serverId, string email);
    Task<TUser?> FindByUsernameAsync(string serverId, string username);
    Task<TUser?> GetUserAsync(string serverId, string userId);
    Task<bool> RegisterAsync(string serverId, TUser user);
    Task<bool> DeleteAccountAsync(string serverId, string userId);
    Task<bool> ChangePasswordAsync(string serverId, string userId, string oldPassword, string newPassword);
    Task<bool> ChangeEmailAsync(string serverId, string userId, string newEmail, string serverConfirmationCode, string clientConfirmationCode);
    Task<bool> IsValidPasswordAsync(string serverId, IServerUser user, string passwordOrPasswordHash);
    string GenerateConfirmationCode(string serverId, string action);
}
