namespace Authentication.Shared;

public record class UserRole(string Name, string AccessKey, string RoleName)
{
    public static readonly UserRole None = new ("None", "None", "None");
}
