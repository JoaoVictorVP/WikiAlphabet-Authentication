using Authentication.Server.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseDefaultServiceProvider(options =>
{
    options.ValidateOnBuild = true;
    options.ValidateScopes = true;
});

Startup.OnServices(builder.Services);

Startup.OnSwaggerStart(builder.Services);

var app = builder.Build();

Startup.OnSwaggerFinish(app);

Startup.OnHTTP(app);

app.Run();