using System;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Authentication.Server.Configuration;
using Authentication.Server.XIdentity.Core.Factories;
using Authentication.Server.XIdentity.Core.Managers;
using Authentication.Server.XIdentity.Core.Repositories;
using Authentication.Server.XIdentity.Core.Services;
using Authentication.Shared.Core.Validators;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Logging;
using Xunit;

namespace Authentication.Server.Tests;

public class AuthenticationControllerTests
{
    private readonly TestServer _server;
    private readonly HttpClient _client;

    public AuthenticationControllerTests()
    {
        TestHelper.Setup(out _server, out _client);
    }

    [Fact]
    public async Task CreateAccount()
    {
        var user = TestFakes.RandomUser();
        
        var response = await _client.PostAsync("/api/authentication/createAccount", JsonContent.Create(user));
        response.EnsureSuccessStatusCode();
    
        var responseContent = await response.Content.ReadFromJsonAsync<dynamic>();
        string json = JsonSerializer.Serialize(responseContent);
        Console.WriteLine(json);
        Assert.NotNull(responseContent);
    }
}