using Application.DTOs;
using FluentValidation;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Validators
{
    public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
    {
        public RegisterUserDtoValidator(ITShopDbContext dbcontext)
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
                        context.AddFailure("PhoneNumber", "Numer telefonu musi składać się z 9 cyfr.");
                    }
                });
            RuleFor(x => x.Password)
                .MinimumLength(6)
                .NotEmpty();
            RuleFor(x => x.ConfirmPassword)
                .Equal(e => e.Password);
            RuleFor(x => x.BirthDay)
                .Must(BeAValidDate).WithMessage("Podano zły format daty");

            RuleFor(x => x.Email)
                .Custom((value, context) =>
{
                    var emailInUse = dbcontext.Users.Any(u => u.Email == value);
                    if (emailInUse)
                    {
                        context.AddFailure("Email", "Ten Email jest już zajęty.");

                    }
                });
        }
        private bool BeAValidDate(DateTime date)
        {
            return !date.Equals(default(DateTime));
        }
    }
}
