using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.Shared.Core.Responses;
public record class AccountResponse(
    string Token,
    string Application,
    DateTime ExpirationDate,
    User User);