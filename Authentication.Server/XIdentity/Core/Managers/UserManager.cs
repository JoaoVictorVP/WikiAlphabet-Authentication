using Authentication.Server.XIdentity.Contracts;
using Authentication.Server.XIdentity.Contracts.Factories;
using Authentication.Server.XIdentity.Contracts.Managers;
using Authentication.Server.XIdentity.Core.Models;
using Authentication.Shared.Contracts.Validators;

namespace Authentication.Server.XIdentity.Core.Managers;

public class UserManager : IUserManager<AppUser>
{
    private readonly IUserFactory _userFactory;
    private readonly IUserValidator _validator;

    public UserManager(IUserFactory userFactory, IUserValidator validator)
    {
        _userFactory = userFactory;
        _validator = validator;
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
        
    }

    public Task<bool> ChangeEmail(string userId, string newEmail) => throw new NotImplementedException();
    public Task<bool> IsValidPassword(string userId, string passwordHash) => throw new NotImplementedException();
}
