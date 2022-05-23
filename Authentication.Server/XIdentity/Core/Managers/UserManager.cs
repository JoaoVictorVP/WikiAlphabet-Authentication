using System.Text;
using Authentication.Server.XIdentity.Contracts.Factories;
using Authentication.Server.XIdentity.Contracts.Managers;
using Authentication.Server.XIdentity.Contracts.Services;
using Authentication.Shared;
using Authentication.Shared.Contracts.Models;
using Authentication.Shared.Contracts.Services.Crypto;
using Authentication.Shared.Contracts.Validators;
using Authentication.Shared.Core.Models.Crypto;

namespace Authentication.Server.XIdentity.Core.Managers;

public class UserManager<TServerUser> : IUserManager<TServerUser> where TServerUser : class, IServerUser
{
    private readonly IUserFactory<TServerUser> _userFactory;
    private readonly IUserValidator _validator;
    private readonly IEmailService _emailService;

    private readonly ICryptoServiceWithPasswordHashing<Salt128, Difficulty> _passwordHashingService;
    private readonly ICryptoServiceWithHashing _hashingService;

    public UserManager(IUserFactory<TServerUser> userFactory, IUserValidator validator, IEmailService emailService, ICryptoServiceWithPasswordHashing<Salt128, Difficulty> passwordHashingService, ICryptoServiceWithHashing hashingService)
    {
        _userFactory = userFactory;
        _validator = validator;
        _emailService = emailService;
        _passwordHashingService = passwordHashingService;
        _hashingService = hashingService;
    }

    public string DoHashPassword(string serverId, string password)
    {
        return _passwordHashingService.HashPassword(password.ToUnicodePlaintext(), 
            new Salt128(Env.Salt), 
            new Difficulty(Env.BCryptWorkFactor)).ToBase64();
    }

    public async Task<TServerUser?> FindByEmailAsync(string serverId, string email)
    {
        var user = await _userFactory.GetUserByEmail(serverId, email) as TServerUser;
        return user;
    }

    public async Task<TServerUser?> FindByUsernameAsync(string serverId, string username)
    {
        var user = await _userFactory.GetUserByUsername(serverId, username) as TServerUser;
        return user;
    }

    public async Task<TServerUser?> GetUserAsync(string serverId, string userId)
    {
        var user = await _userFactory.GetUser(serverId, userId) as TServerUser;
        return user;
    }

    public async Task<bool> RegisterAsync(string serverId, TServerUser user)
    {
        user.Password = DoHashPassword(serverId, user.Password);

        var validation = _validator.Validate(user);
        if (!validation.IsValid)
            throw new Exception(validation.ToString());

        await _userFactory.AddUser(serverId, user);

        return true;
    }

    public async Task<bool> DeleteAccountAsync(string serverId, string userId)
    {
        await _userFactory.RemoveUser(serverId, userId);
        return true;
    }

    public async Task<bool> ChangePasswordAsync(string serverId, string userId, string oldPassword, string newPassword)
    {
        if (await _userFactory.GetUser(serverId, userId) is not TServerUser user)
            throw new Exception($"Cannot find user {userId}");
        oldPassword = DoHashPassword(serverId, oldPassword);
        bool passwordsSame = user.Password == oldPassword;
        if (passwordsSame)
        {
            user.Password = DoHashPassword(serverId, newPassword);
            await _userFactory.UpdateUser(serverId, userId, user);
            await _emailService.SendEmail(user.Email, "Password Changed", Emails.PasswordChanged());
            return true;
        }
        else
        {
            throw new Exception("Old password is incorrect");
        }
    }

    public async Task<bool> ChangeEmailAsync(string serverId, string userId, string newEmail, string serverConfirmationCode, string clientConfirmationCode)
    {
        if(serverConfirmationCode != clientConfirmationCode)
            throw new Exception("Server and client confirmation codes do not match");
        if (await _userFactory.GetUser(serverId, userId) is not TServerUser user)
            throw new Exception($"Cannot find user {userId}");
        user.Email = newEmail;
        await _userFactory.UpdateUser(serverId, userId, user);
        await _emailService.SendEmail(user.Email, "Email Changed", Emails.EmailChanged());
        return true;
    }
    
    public Task<bool> IsValidPasswordAsync(string serverId, IServerUser user, string passwordOrPasswordHash)
    {
        if(user is null || passwordOrPasswordHash is null or "")
            throw new Exception("User or password cannot be null");
        bool valid = user.Password == passwordOrPasswordHash || user.Password == DoHashPassword(serverId, passwordOrPasswordHash);
        return Task.FromResult(valid);
    }

    public string GenerateConfirmationCode(string serverId, string action)
    {
        string confirm = action + DateTime.UtcNow.ToString();
        var confirmBytes = Encoding.UTF8.GetBytes(confirm);
        var salt = Env.Salt;
        var hash = _hashingService.HMAC(salt, confirmBytes).ToHex().ToUpper();
        return hash[..6];
    }
}
