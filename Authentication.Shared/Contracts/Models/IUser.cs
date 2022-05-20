namespace Authentication.Shared.Contracts;

public interface IUser
{
    string Id { get; set; }
    string Name { get; set; }
    string Username { get; set; }
    string Email { get; set; }
    string Password { get; set; }

    List<UserRole> UserRoles { get; set; }

    bool Active { get; set; }
    DateTime CreatedDate { get; set; }

    bool Deleted { get; set; }
    DateTime? DeletedDate { get; set; }
}