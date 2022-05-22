using Authentication.Shared.Contracts;
using Authentication.Shared.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.Shared.Core.Responses;
public record class AccountResponse<TServerUser>(
    string Token,
    string Application,
    DateTime ExpirationDate,
    TServerUser User) where TServerUser : IServerUser;