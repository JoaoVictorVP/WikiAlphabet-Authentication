using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.Shared.Core.Requests;
public class LoginRequest
{
    public string? Username { get; set; }
    [DataType(DataType.EmailAddress)]
    public string? Email { get; set; }
    [Required, DataType(DataType.Password)]
    public string Password { get; set; } = "";

    public void Deconstruct(out string? username, out string? email, out string password)
    {
        username = Username;
        email = Email;
        password = Password;
    }
}