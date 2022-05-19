using System.Text;
using Authentication.Server.XIdentity.Contracts;
using Authentication.Server.XIdentity.Contracts.Factories;
using Authentication.Server.XIdentity.Contracts.Managers;
using Authentication.Server.XIdentity.Contracts.Services;
using Authentication.Server.XIdentity.Core.Models;
using Authentication.Shared;
using Authentication.Shared.Contracts.Validators;

namespace Authentication.Server.XIdentity.Core.Managers;

public class UserManager : IUserManager<AppUser>
{
    private readonly IUserFactory _userFactory;
    private readonly IUserValidator _validator;
    private readonly IEmailService _emailService;

    public UserManager(IUserFactory userFactory, IUserValidator validator, IEmailService emailService)
    {
        _userFactory = userFactory;
        _validator = validator;
        _emailService = emailService;
    }

    public string DoHashPassword(string password)
    {
        return CryptoUtils.HashPassword(password, Env.Salt, Env.BCryptWorkFactor);
    }

    public async Task<AppUser?> FindByEmailAsync(string email)
    {
        var user = await _userFactory.GetUserByEmail(email) as AppUser;
        return user;
    }

    public async Task<AppUser?> FindByUsernameAsync(string username)
    {
        var user = await _userFactory.GetUserByUsername(username) as AppUser;
        return user;
    }

    public async Task<AppUser?> GetUserAsync(string userId)
    {
        var user = await _userFactory.GetUser(userId) as AppUser;
        return user;
    }

    public async Task<bool> RegisterAsync(AppUser user)
    {
        user.User.Password = DoHashPassword(user.User.Password);
        var validation = _validator.Validate(user.User);
        if (!validation.IsValid)
            throw new Exception(validation.ToString());

        await _userFactory.AddUser(user);

        return true;
    }

    public async Task<bool> DeleteAccountAsync(string userId)
    {
        await _userFactory.RemoveUser(userId);
        return true;
    }

    public async Task<bool> ChangePasswordAsync(string userId, string oldPassword, string newPassword)
    {
        if (await _userFactory.GetUser(userId) is not AppUser user)
            throw new Exception($"Cannot find user {userId}");
        oldPassword = DoHashPassword(oldPassword);
        bool passwordsSame = user.PasswordHash == oldPassword;
        if (passwordsSame)
        {
            user.User.Password = DoHashPassword(newPassword);
            await _userFactory.UpdateUser(userId, user);
            await _emailService.SendEmail(user.Email, "Password Changed", Emails.PasswordChanged());
            return true;
        }
        else
        {
            throw new Exception("Old password is incorrect");
        }
    }

    public async Task<bool> ChangeEmailAsync(string userId, string newEmail, string serverConfirmationCode, string clientConfirmationCode)
    {
        if(serverConfirmationCode != clientConfirmationCode)
            throw new Exception("Server and client confirmation codes do not match");
        if (await _userFactory.GetUser(userId) is not AppUser user)
            throw new Exception($"Cannot find user {userId}");
        user.Email = newEmail;
        await _userFactory.UpdateUser(userId, user);
        await _emailService.SendEmail(user.Email, "Email Changed", Emails.EmailChanged());
        return true;
    }
    
    public Task<bool> IsValidPasswordAsync(IUser user, string passwordOrPasswordHash)
    {
        if(user is null || passwordOrPasswordHash is null or "")
            throw new Exception("User or password cannot be null");
        bool valid = user.PasswordHash == passwordOrPasswordHash || user.PasswordHash == DoHashPassword(passwordOrPasswordHash);
        return Task.FromResult(valid);
    }

    public string GenerateConfirmationCode(string action)
    {
        string confirm = action + DateTime.UtcNow.ToString();
        var confirmBytes = Encoding.UTF8.GetBytes(confirm);
        var salt = Env.Salt;
        var hash = CryptoUtils.HashWithMAC(salt, confirmBytes);
        return hash.Substring(0, 6);
    }
}
