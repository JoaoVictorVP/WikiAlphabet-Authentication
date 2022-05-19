using Authentication.Server.Configuration;

var builder = WebApplication.CreateBuilder(args);

Startup.OnServices(builder.Services);

Startup.OnSwaggerStart(builder.Services);

var app = builder.Build();

Startup.OnSwaggerFinish(app);

Startup.OnHTTP(app);

app.Run();