using Authentication.Server.Contracts.Services;
using Authentication.Server.Databases.Contracts;
using Authentication.Server.Databases.Core;
using Authentication.Server.Services;
using Authentication.Server.XIdentity.Contracts;
using Authentication.Server.XIdentity.Contracts.Factories;
using Authentication.Server.XIdentity.Contracts.Managers;
using Authentication.Server.XIdentity.Contracts.Repositories;
using Authentication.Server.XIdentity.Contracts.Services;
using Authentication.Server.XIdentity.Core.Factories;
using Authentication.Server.XIdentity.Core.Managers;
using Authentication.Server.XIdentity.Core.Repositories;
using Authentication.Server.XIdentity.Core.Services;
using Authentication.Shared;
using Authentication.Shared.Contracts.Services.Crypto;
using Authentication.Shared.Contracts.Validators;
using Authentication.Shared.Core.Models;
using Authentication.Shared.Core.Models.Crypto;
using Authentication.Shared.Core.Services.Crypto;
using Authentication.Shared.Core.Validators;
using LiteDB;
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

        // Authentication
        services.AddTransient<ITokenService, JWTokenService>();

        services.AddTransient<IUserManager<defServerUser>, defUserManager>();
        services.AddTransient<IUserFactory<defServerUser>, UserFactory<defServerUser>>();
        services.AddSingleton<IUserRepository<defServerUser>, UserRepository<defServerUser>>();

        services.AddTransient<IRoleManager<Role>, RoleManager>();

        services.AddTransient<IEmailService, EmailService>();

        // Cryptography
        services.AddTransient<ICryptoServiceWithHashing, SHA256CryptoService>();
        services.AddTransient<ICryptoServiceWithPasswordHashing<Salt128, Difficulty>, BCryptHashingCryptoService>();
    }

    public static void AddDatabases(this IServiceCollection services, IConfiguration configuration)
    {
        // Databases
        services.AddTransient<IDatabaseFactory<LiteDatabase>, LiteDBFactory>(
            x => new LiteDBFactory(configuration.GetConnectionString("LiteDB")));
    }

    public static void ConfigureAuth(this IApplicationBuilder app)
    {
        app.UseAuthentication();
        app.UseAuthorization();
    }

    public static void AddValidators(this IServiceCollection services)
    {
        services.AddTransient<IUserValidator, UserValidator>();
    }
}
