﻿using FluentValidation;

namespace Ambev.Application.UseCases.Commands.User.UpdateUser;

public sealed class UpdateUserValidator : AbstractValidator<UpdateUserRequest>
{
    public UpdateUserValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Id).GreaterThan(0);
        RuleFor(x => x.Username).NotEmpty().MaximumLength(200);
        RuleFor(x => x.Email).NotEmpty().MinimumLength(3).MaximumLength(50);
        RuleFor(x => x.Email).EmailAddress();
    }
}
