using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Authentication.Shared;

namespace Authentication.Server.Tests;

public static class TestFakes
{
    static Bogus.Randomizer random = new ();
    static Bogus.DataSets.Name names = new ();
    static Bogus.DataSets.Internet internet = new ();
    static Bogus.DataSets.Database database = new ();
    static Bogus.DataSets.Rant rant = new ();

    public static User RandomUser()
    {
        return new User
        {
            Name = names.FullName(),
            Username = internet.UserName(),
            Email = internet.Email(),
            Password = internet.Password(),
            UserRoles = new() { new UserRole("wisocialmedia", "socialMedia@!", "Admin") }
        };
    }
}