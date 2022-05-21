using static System.Console;
using static Authentication.Client.MenuHelper;
using EasyConsoleCore;
using Authentication.Shared;
using Authentication.Client;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Authentication.Shared.Core.Requests;
using System.Dynamic;
using Authentication.Shared.Core.Responses;
using Authentication.Shared.Core.Services.Crypto;
using Authentication.Shared.Core.Models.Crypto;

var passwordHashingService = new BCryptHashingCryptoService();
var salt = new Salt128(Env.Salt);
var difficulty = new Difficulty(Env.Difficulty);

var clientHandler = new HttpClientHandler();

clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

HttpClient client = new (clientHandler);

string url = Input("Url do site: ", "https://localhost:5001/");
if(url is null or "")
    throw new ArgumentException("Url não pode ser vazia", nameof(url));

while(true)
{
    var menu = new Menu()
        .Add("Login", () => DisplayLogin().Wait())
        .Add("Registrar", () => DisplayRegister().Wait())
        .Add("Testar", () => DisplayTest().Wait())
        .Add("Logout", DisplayLogout);
    menu.Display();
    Pause();
    Clear();
}

void AuthorizeClient(string token)
{
    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
}

async Task DisplayLogin()
{
    var username = Input("Usuário: ");
    var password = Input("Senha: ");
    var request = new LoginRequest()
    {
        Username = username,
        Password = passwordHashingService.HashPassword(password.ToUnicodePlaintext(), salt, difficulty).ToBase64()
    };
    var response = await client.PostAsJsonAsync($"{url}api/authentication/login", request);
    if(response.IsSuccessStatusCode)
    {
        var token = (await response.Content.ReadFromJsonAsync<AccountResponse<User>>())!.Token;
        AuthorizeClient(token);
        WriteLine("Login realizado com sucesso!");
    }
    else
    {
        WriteLine("Falha ao realizar login");
    }
}
async Task DisplayRegister()
{
    try
    {
        string name = Input("Nome: ");
        string email = Input("Email: ");
        string username = Input("Nome de usuário: ");
        string password = Input("Senha: ");
        
        var user = new User
        {
            Id = Guid.NewGuid().ToString(),
            Name = name,
            Email = email,
            Username = username,
            Password = passwordHashingService.HashPassword(password.ToUnicodePlaintext(), salt, difficulty).ToBase64(),
            Active = true,
            CreatedDate = DateTime.Now,
            Deleted = false,
            DeletedDate = null,
            UserRoles = new List<UserRole>()
        };

        var accountResult = await client.PostAsync(url + "api/authentication/createAccount", JsonContent.Create(user));
        accountResult.EnsureSuccessStatusCode();
        var account = await accountResult.Content.ReadFromJsonAsync<AccountResponse<User>>();
        AuthorizeClient(account!.Token);

        WriteLine($"Você foi registrado com sucesso como {username}");
    }
    catch(Exception ex)
    {
        WriteLine($"Falha ao registrar usuário: {ex.Message}");
    }
}
async Task DisplayTest()
{
    var response = await client.GetAsync(url + "api/authentication/test");
    if(response.IsSuccessStatusCode)
    {
        var result = await response.Content.ReadAsStringAsync();
        WriteLine(result ?? "<NULL RESPONSE MESSAGE>");
    }
    else
    {
        WriteLine("Falha ao realizar teste");
    }
}
void DisplayLogout()
{
    client.DefaultRequestHeaders.Authorization = null;
    WriteLine("Logout realizado com sucesso!");
}