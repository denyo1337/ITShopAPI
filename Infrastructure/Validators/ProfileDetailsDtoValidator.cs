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
    public class ProfileDetailsDtoValidator : AbstractValidator<ProfileDetailsDto>
    {
        public ProfileDetailsDtoValidator(ITShopDbContext dbContext)
        {
            RuleFor(x => x.NickName)
                .Custom((value,context)=>
                {
                    if (!string.IsNullOrWhiteSpace(value))
                    {
                        if(value.Length < 3)
                        {
                            context.AddFailure("Pseudonim musi zawierać przynajmniej 3 znaki.");
                        }
                    }
                })
                .MaximumLength(28);
            RuleFor(x => x.Name)
                 .Custom((value, context) =>
                 {
                     if (!string.IsNullOrWhiteSpace(value))
                     {
                         if (value.Length < 3)
                         {
                             context.AddFailure("Polskie imiona składają się przynajmniej z 3 znaków");
                         }
                     }
                 })
                .MaximumLength(28);
            RuleFor(x => x.Nationality)
                .MaximumLength(58);
            RuleFor(x => x.PhoneNumber.ToString())
                .Length(9);
            RuleFor(x => x.SurrName)
                .Custom((value, context) =>
                {
                    if (!string.IsNullOrWhiteSpace(value))
                    {
                        if (value.Length < 3)
                        {
                            context.AddFailure("Polskie nazwiska składają się przynajmniej z 3 znaków");
                        }
                    }
                })
                .MaximumLength(58);
            RuleFor(x => x.BirthDay)
                .Must(BeAValidAge).WithMessage("Data musi być w formacie {dd/MM/yyyy}");
        }
        private bool BeAValidAge(DateTime? date)

            {
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
