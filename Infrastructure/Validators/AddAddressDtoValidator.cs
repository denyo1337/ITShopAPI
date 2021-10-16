using Application.DTOs;
using Application.Exceptions;
using FluentValidation;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Validators
{
    public class AddAddressDtoValidator : AbstractValidator<AddressDto>
    {
        public AddAddressDtoValidator(ITShopDbContext dbcontext)
        {
            RuleFor(x => x.Street)
                   .NotEmpty()
                   .MaximumLength(200);
            RuleFor(x => x.Towm)
                .NotEmpty()
                .MaximumLength(200);
            RuleFor(x => x.PostalCode)
                .Length(6)
                .WithMessage("Podany zły format kodu pocztowego")
                .Custom((value, context) =>
                {
                    int leftParse = 0;
                    if (!value.Contains("-"))
                    {
                        throw new BadFormatException("Podano zły format kodu pocztowego");
                    }
                    var parts = value.Split("-");
                    var leftSide = int.TryParse(parts[0], out leftParse);
                    var rightSide = int.TryParse(parts[1], out leftParse);
                    if (parts[0].Length!=2 || parts[1].Length!=3 || !leftSide || !rightSide)
                    {
                        throw new BadFormatException("Podano zły format kodu pocztowego");
                    }
                });
            RuleFor(x => x.Country)
                .NotEmpty()
                .MaximumLength(200);
        }
    }
}
