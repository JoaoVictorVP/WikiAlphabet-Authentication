using Authentication.Shared.Contracts;
using Authentication.Shared.Contracts.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Shared.Core.Validators;

public class UserValidator : AbstractValidator<IUser>, IUserValidator
{
    public UserValidator()
    {
        RuleFor(x => x.Name).NotNull().NotEmpty().MinimumLength(1);
        RuleFor(x => x.Username).NotNull().NotEmpty().MinimumLength(1);
        RuleFor(x => x.Email).EmailAddress();
        RuleFor(x => x.CreatedDate).GreaterThan(DateTime.MinValue).LessThan(DateTime.MaxValue);
        RuleFor(x => x.Deleted).Equal(false);
    }
}
