using Application.DTOs;
using FluentValidation;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Validators
{
    public class RegosterUserDtoValidator : AbstractValidator<RegisterUserDto>
    {
        public RegosterUserDtoValidator(ITShopDbContext context)
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(25);
            RuleFor(x => x.SurrName)
                .NotEmpty()
                .MaximumLength(25);
            RuleFor(x => x.PhoneNumber)
                .Custom((value, context) =>
                {
                    if (value.Value.ToString().Length != 9)
                    {
                        context.AddFailure("PhoneNumber", "Numer telefonu składa się z 9 cyfr.");
                    }
                });
            RuleFor(x => x.Password)
                .MinimumLength(6)
                .NotEmpty();
            RuleFor(x => x.ConfirmPassword)
                .Equal(e => e.Password);
            RuleFor(x => x.BirthDay)
                .Must(e => e.Value.GetType() == typeof(DateTime));
        }
    }
}
