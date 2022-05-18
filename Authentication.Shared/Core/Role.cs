﻿namespace Authentication.Shared;

public class Role
{
    public string Name { get; set; } = "";
    public List<Permission> Permissions { get; set; } = new(32);

    public Role(string role)
    {
        Name = role;
    }
}