using Authentication.Server.XIdentity.Contracts;
using Authentication.Server.XIdentity.Contracts.Managers;
using Authentication.Server.XIdentity.Contracts.Repositories;
using Authentication.Server.XIdentity.Core.Managers;
using Authentication.Server.XIdentity.Core.Models;
using Authentication.Server.XIdentity.Core.Repositories;
using Authentication.Shared;
using Authentication.Shared.Contracts.Validators;
using Authentication.Shared.Core.Validators;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Authentication.Server.Configuration;

public static class ApplicationExtensions
{
    public static void AddAuthServices(this IServiceCollection services)
    {
        var key = new SymmetricSecurityKey(Env.Secret);
        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(x =>
        {
            x.RequireHttpsMetadata = true;
            x.SaveToken = true;
            x.TokenValidationParameters = new ()
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = key,
                ValidateIssuer = false,
                ValidateAudience = false
            };
        });

        services.AddTransient<IUserManager<AppUser>, UserManager>();
        services.AddTransient<IUserRepository, UserRepository>();

        services.AddTransient<IRoleManager<Role>, RoleManager>();
    }

    public static void ConfigureAuth(this WebApplication app)
    {
        app.UseAuthentication();
        app.UseAuthorization();
    }

    public static void AddValidators(this IServiceCollection services)
    {
        services.AddTransient<IUserValidator, UserValidator>();
    }
}
