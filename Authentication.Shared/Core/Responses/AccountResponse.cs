using Authentication.Shared.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.Shared.Core.Responses;
public record class AccountResponse<TUser>(
    string Token,
    string Application,
    DateTime ExpirationDate,
    TUser User) where TUser : IUser;