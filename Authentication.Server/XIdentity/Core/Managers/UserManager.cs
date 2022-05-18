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

    public async Task<bool> Register(AppUser user)
    {
        var validation = _validator.Validate(user.User);
        if (validation.IsValid is not true)
            throw new Exception(validation.ToString());

        await _userFactory.AddUser(user);

        return true;
    }

    public async Task<bool> DeleteAccount(string userId)
    {
        await _userFactory.RemoveUser(userId);
        return true;
    }

    public async Task<bool> ChangePassword(string userId, string oldPassword, string newPassword)
    {
        var user = await _userFactory.GetUser(userId) as AppUser;
        if(user is null)
            throw new Exception($"Cannot find user {userId}");
        bool passwordsSame = user.PasswordHash == oldPassword || user.PasswordHash == CryptoUtils.HashPassword(oldPassword, Env.Salt);
        if(user.PasswordHash == oldPassword)
        {
            user.User.Password = newPassword;
            await _userFactory.UpdateUser(userId, user);
            await _emailService.SendEmail(user.Email, "Password Changed", Emails.PasswordChanged());
            return true;
        }
        else
            throw new Exception("Old password is incorrect");
    }

    public async Task<bool> ChangeEmail(string userId, string newEmail, string serverConfirmationCode, string clientConfirmationCode)
    {
        if(serverConfirmationCode != clientConfirmationCode)
            throw new Exception("Server and client confirmation codes do not match");
        var user = await _userFactory.GetUser(userId) as AppUser;
        if(user is null)
            throw new Exception($"Cannot find user {userId}");
        user.Email = newEmail;
        await _userFactory.UpdateUser(userId, user);
        await _emailService.SendEmail(user.Email, "Email Changed", Emails.EmailChanged());
        return true;
    }
    
    public async Task<bool> IsValidPassword(string userId, string passwordHash)
    {
        if(userId is null or "" || passwordHash is null or "")
            throw new Exception("UserId or passwordHash is null or empty");
        var user = await _userFactory.GetUser(userId) as AppUser;
        if(user is null)
            throw new Exception($"Cannot find user {userId}");
        return user.PasswordHash == passwordHash;
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
