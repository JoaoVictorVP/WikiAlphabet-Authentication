using Authentication.Shared;
using Authentication.Shared.Contracts.Validators;
using Authentication.Shared.Core.Validators;

namespace Authentication.Server.Configuration;

public static class ApplicationExtensions
{
    public static void AddAuthServices(this IServiceCollection services)
    {
        
    }

    public static void AddValidators(this IServiceCollection services)
    {
        services.AddTransient<IUserValidator, UserValidator>();
    }
}
