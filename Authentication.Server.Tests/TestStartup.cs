using System;
using Authentication.Server.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Authentication.Server.Tests;

public class TestStartup : IStartup
{
    public void Configure(IApplicationBuilder app)
    {
        app.ConfigureAuth();
        var built = app.Build();
        app.Run(built);
    }

    public IServiceProvider ConfigureServices(IServiceCollection services)
    {
        Startup.OnServices(services);

        return services.BuildServiceProvider();
    }
}
