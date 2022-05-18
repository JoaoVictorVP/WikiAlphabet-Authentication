using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Shared.Contracts;

public interface IRole
{
    string Name { get; }
    IList<IClaim> Claims { get; set; }
}
