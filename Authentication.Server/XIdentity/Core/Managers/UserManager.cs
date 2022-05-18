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

    public bool Register(IUser user)
    {
        
    }

    public bool DeleteAccount(string userId)
    {
        throw new NotImplementedException();
    }

    public bool ChangePassword(string userId, string oldPassword, string newPassword)
    {
        throw new NotImplementedException();
    }

    public bool ChangeEmail(string userId, string newEmail)
    {
        throw new NotImplementedException();
    }

    public bool IsValidPassword(string userId, string passwordHash)
    {
        throw new NotImplementedException();
    }
}
