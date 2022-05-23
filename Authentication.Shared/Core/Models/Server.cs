using Authentication.Shared.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Shared.Core.Models;

public class Server : IServer
{
    public string Id { get; set; } = "";
    public string Email { get; set; } = "";
    public string Password { get; set; } = "";
    public string OnUserRegisteredEndpoint { get; set; } = "";
    public string OnUserLogggedInEndpoint { get; set; } = "";
    public string GetUserTokenEndpoint { get; set; } = "";
    public string OnUserDeletedAccountEndpoint { get; set; } = "";
}
