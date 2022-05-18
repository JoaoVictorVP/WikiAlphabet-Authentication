using AspNetCore.Identity.LiteDB;
using Authentication.Shared;

namespace Authentication.Server.Configuration;

public static class ApplicationExtensions
{
    public static void AddAuthServices(this IServiceCollection services)
    {
        
    }

    public static void ConfigureIdentity(this IServiceCollection services)
    {
        services.AddIdentity<User, Role>(options =>
        {
            options.User = new()
            {
                RequireUniqueEmail = true
            };
            options.Password = new()
            {
                RequireDigit = false,
                RequiredLength = 6,
                RequiredUniqueChars = 0,
                RequireLowercase = false,
                RequireNonAlphanumeric = false,
                RequireUppercase = false
            };
            options.Lockout = new()
            {
                AllowedForNewUsers = true,
                MaxFailedAccessAttempts = 5,
                DefaultLockoutTimeSpan = TimeSpan.FromDays(5)
            };
        })
        .AddUserStore<LiteDbUserStore<ApplicationUser>>();
    }
}
