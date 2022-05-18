﻿using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Shared.Contracts;

public interface IModelValidator<T>
{
    ValidationResult Validate(T model);
}
