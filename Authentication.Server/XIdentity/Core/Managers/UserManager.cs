using System.Text;
using Authentication.Server.XIdentity.Contracts;
using Authentication.Server.XIdentity.Contracts.Factories;
using Authentication.Server.XIdentity.Contracts.Managers;
using Authentication.Server.XIdentity.Contracts.Services;
using Authentication.Server.XIdentity.Core.Models;
using Authentication.Shared;
using Authentication.Shared.Contracts.Services.Crypto;
using Authentication.Shared.Contracts.Validators;
using Authentication.Shared.Core.Models.Crypto;

namespace Authentication.Server.XIdentity.Core.Managers;

public class UserManager<TServerUser> : IUserManager<TServerUser> where TServerUser : class, IServerUser
{
    private readonly IUserFactory _userFactory;
    private readonly IUserValidator _validator;
    private readonly IEmailService _emailService;
    private readonly ICryptoServiceWithPasswordHashing<Salt128, Difficulty> _passwordHashingService;

    public UserManager(IUserFactory userFactory, IUserValidator validator, IEmailService emailService, ICryptoServiceWithPasswordHashing<Salt128, Difficulty> passwordHashingService)
    {
        _userFactory = userFactory;
        _validator = validator;
        _emailService = emailService;
        _passwordHashingService = passwordHashingService;
    }

    public string DoHashPassword(string password)
    {
        return _passwordHashingService.HashPassword(password.ToUnicodePlaintext(), 
            new Salt128(Env.Salt), 
            new Difficulty(Env.BCryptWorkFactor)).ToBase64();
    }

    public async Task<TServerUser?> FindByEmailAsync(string email)
    {
        var user = await _userFactory.GetUserByEmail(email) as TServerUser;
        return user;
    }

    public async Task<TServerUser?> FindByUsernameAsync(string username)
    {
        var user = await _userFactory.GetUserByUsername(username) as TServerUser;
        return user;
    }

    public async Task<TServerUser?> GetUserAsync(string userId)
    {
        var user = await _userFactory.GetUser(userId) as TServerUser;
        return user;
    }

    public async Task<bool> RegisterAsync(TServerUser user)
    {
        user.Password = DoHashPassword(user.Password);

        if (user is IBackingSharedUser backingUser)
        {
            var validation = _validator.Validate(backingUser.User);
            if (!validation.IsValid)
                throw new Exception(validation.ToString());
        }

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
        if (await _userFactory.GetUser(userId) is not TServerUser user)
            throw new Exception($"Cannot find user {userId}");
        oldPassword = DoHashPassword(oldPassword);
        bool passwordsSame = user.Password == oldPassword;
        if (passwordsSame)
        {
            user.Password = DoHashPassword(newPassword);
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
        if (await _userFactory.GetUser(userId) is not TServerUser user)
            throw new Exception($"Cannot find user {userId}");
        user.Email = newEmail;
        await _userFactory.UpdateUser(userId, user);
        await _emailService.SendEmail(user.Email, "Email Changed", Emails.EmailChanged());
        return true;
    }
    
    public Task<bool> IsValidPasswordAsync(IServerUser user, string passwordOrPasswordHash)
    {
        if(user is null || passwordOrPasswordHash is null or "")
            throw new Exception("User or password cannot be null");
        bool valid = user.Password == passwordOrPasswordHash || user.Password == DoHashPassword(passwordOrPasswordHash);
        return Task.FromResult(valid);
    }

    public string GenerateConfirmationCode(string action)
    {
        string confirm = action + DateTime.UtcNow.ToString();
        var confirmBytes = Encoding.UTF8.GetBytes(confirm);
        var salt = Env.Salt;
        var hash = CryptoUtils.HashWithMAC(salt, confirmBytes);
        return hash[..6];
    }
}
