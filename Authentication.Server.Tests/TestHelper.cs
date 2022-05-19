using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Authentication.Server.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;

namespace Authentication.Server.Tests;
public static class TestHelper
{
    public static void Setup(out TestServer server, out HttpClient client)
    {
        var builder = new WebHostBuilder();;

        builder.UseStartup<TestStartup>();
        
        server = new TestServer(builder);

        client = server.CreateClient();
    }
    public static void SetupAuthentication(HttpClient client, string token, string authType = "Bearer")
    {
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(authType, token);
    }
}
