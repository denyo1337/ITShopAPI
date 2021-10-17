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
    public class CreateUserDtoValidator : AbstractValidator<CreateUserDto>
    {
        public CreateUserDtoValidator(ITShopDbContext dbContext)
        {
            RuleFor(x => x.Email)
                .EmailAddress()
                .Custom((value, context) =>
                {
                    var isEmailTaken = dbContext.Users.Any(x=>x.Email == value);
                    if (isEmailTaken)
                    {
                        context.AddFailure($"Email o adresie {value} jest zajęty.");
                    }
                });
            RuleFor(x => x.Password)
                .Equal(e => e.ConfirmPassword);
            RuleFor(x => x.NickName)
                .MinimumLength(3)
                .Custom((value, context) =>
                {
                    var isTaken = dbContext.Users.Any(x => x.NickName == value);
                    if (isTaken)
                    {
                        context.AddFailure($"Nick {value} jest zajęty");
                    }
                });
            RuleFor(x => x.IsActive)
                .NotNull();
            RuleFor(x => x.IsBanned)
                .NotNull();

            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(28);
            RuleFor(x => x.SurrName)
                .NotEmpty()
                .MaximumLength(28);
            RuleFor(x => x.RoleId)
                .InclusiveBetween(1, 3);
            RuleFor(x => x.BirthDay)
                .Must(BeAValidAge).WithMessage("Podaj poprawny format daty {dd/MM/yyyy}");

        }
        private bool BeAValidAge(DateTime? date)
        {
            if (date == null)
            {
                return true;
            }
            int currentYear = DateTime.Now.Year;
            int dobYear = date.Value.Year;

            if (dobYear <= currentYear && dobYear > (currentYear - 120))
            {
                return true;
            }
            return false;
        }
    }
}
