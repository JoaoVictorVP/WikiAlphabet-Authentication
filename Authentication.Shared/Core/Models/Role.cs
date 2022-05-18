using Authentication.Shared.Contracts;

namespace Authentication.Shared;

public class Role : IRole
{
    public string Name { get; set; } = "";
    public List<IClaim> Claims { get; set; } = new(32);

    public Role(string role)
    {
        Name = role;
    }

    IList<IClaim> IRole.Claims { get => Claims; set => Claims = value; }
}
