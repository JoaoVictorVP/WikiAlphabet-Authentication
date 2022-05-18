using Authentication.Server.XIdentity.Contracts;
using Authentication.Server.XIdentity.Contracts.Factories;
using Authentication.Server.XIdentity.Contracts.Managers;
using Authentication.Server.XIdentity.Core.Models;

namespace Authentication.Server.XIdentity.Core.Managers;

public class UserManager : IUserManager<AppUser>
{
    private readonly IUserFactory _userRepository;

    public UserManager(IUserFactory userRepository)
    {
        _userRepository = userRepository;
    }

    public bool Register(IUser user)
    {
        throw new NotImplementedException();
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
