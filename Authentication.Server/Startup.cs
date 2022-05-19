using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.Server.Configuration;

public static class Startup
{
    public static void OnServices(IServiceCollection services)
    {
        // Add services to the container.
        services.AddAuthServices();
        services.AddControllers();
        services.AddValidators();
    }

    public static void OnSwaggerStart(IServiceCollection services)
    {
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }
    public static void OnSwaggerFinish(WebApplication app)
    {
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
    }

    public static void OnHTTP(WebApplication app)
    {
        app.UseHttpsRedirection();

        app.ConfigureAuth();

        app.MapControllers();
    }
}