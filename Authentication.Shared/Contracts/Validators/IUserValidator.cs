using Authentication.Shared.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Shared.Contracts.Validators;

public interface IUserValidator : IModelValidator<IServerUser>
{
}
