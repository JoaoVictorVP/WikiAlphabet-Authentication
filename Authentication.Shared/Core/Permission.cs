namespace Authentication.Shared;

public record class Permission(string Name, PermissionLevel Level);

public enum PermissionLevel
{
    ReadOnly,
    WriteOnly,
    WriteAndRead
}