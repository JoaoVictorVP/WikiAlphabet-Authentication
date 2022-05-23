﻿using Authentication.Shared.Contracts;
using Authentication.Shared.Contracts.Models;
using Authentication.Shared.Contracts.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Shared.Core.Validators;

public class UserValidator : AbstractValidator<IServerUser>, IUserValidator
{
    public UserValidator()
    {
        RuleFor(x => x.FullName).NotNull().NotEmpty().MinimumLength(1);
        RuleFor(x => x.Username).NotNull().NotEmpty().MinimumLength(1);
        RuleFor(x => x.Email).EmailAddress();

        RuleFor(x => x.CreatedAt).GreaterThan(DateTime.MinValue).LessThan(DateTime.MaxValue);
    }
}
