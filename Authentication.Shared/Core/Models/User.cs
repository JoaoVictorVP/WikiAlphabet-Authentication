using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Shared;

public class User
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; } = "";
    public string Username { get; set; } = "";
    public string Email { get; set; } = "";
    public string Password { get; set; } = "";
    
    public List<UserRole> UserRoles { get; set; } = new (32);

    public bool Active { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.Now;

    public bool Deleted { get; set; }
    public DateTime? DeletedDate { get; set; } = null;
}
