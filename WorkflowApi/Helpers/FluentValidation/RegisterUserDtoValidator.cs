﻿using FluentValidation;
using WorkflowApi.Data;
using WorkflowApi.DataTransferObject;

namespace WorkflowApi.Models.Validators
{
    public class RegisterUserDtoValidator :AbstractValidator<RegisterUserDto>
    {
        public RegisterUserDtoValidator(ApplicationDbContext dbContext)
        {

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Password).MinimumLength(6);

            //RuleFor(x => x.Email)
            //    .Custom((value, context) =>
            //    {

            //        var emailInUse = dbContext.Users.Any(u => u.Email == value);
            //        if (emailInUse)
            //        {
            //            context.AddFailure("Email", "That email is taken");
            //        }
            //    });
        }
    }
}
