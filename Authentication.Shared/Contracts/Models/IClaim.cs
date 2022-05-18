using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Shared.Contracts;

public interface IClaim
{
    string Name { get; }
    ClaimLevel Level { get; }
}

public enum ClaimLevel
{
    ReadOnly,
    WriteOnly,
    WriteAndRead
}