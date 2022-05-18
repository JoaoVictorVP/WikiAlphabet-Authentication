using Authentication.Shared.Contracts;
using Authentication.Shared.Contracts.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Shared.Core.Validators;

public class UserValidator : AbstractValidator<User>, IUserValidator
{
    public UserValidator()
    {
        RuleFor(x => x.Name).NotNull().NotEmpty().MinimumLength(3);
        RuleFor(x => x.Username).NotNull().NotEmpty().MinimumLength(6);
        RuleFor(x => x.Email).EmailAddress();
    }
}
