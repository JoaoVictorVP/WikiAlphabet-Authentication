
global using defUser = Authentication.Shared.User;

global using defServerUser = Authentication.Server.XIdentity.Core.Models.AppUser
    <Authentication.Shared.User>;

global using defUserManager = Authentication.Server.XIdentity.Core.Managers.UserManager
    <Authentication.Server.XIdentity.Core.Models.AppUser
    <Authentication.Shared.User>>;