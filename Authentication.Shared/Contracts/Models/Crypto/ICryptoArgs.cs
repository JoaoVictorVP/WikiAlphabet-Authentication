using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Shared.Contracts.Models;

public interface ICryptoArgs
{
    void Generic(string name, object value);
    T Get<T>(string name);
}
