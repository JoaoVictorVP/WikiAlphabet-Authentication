using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Authentication.Client;
public static class Env
{
    public static readonly byte[] Salt = RandomNumberGenerator.GetBytes(32);
    public static readonly int Difficulty = 10;
}