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
                .Custom((value, context) =>
                {
                    int leftParse = 0;
                    var parts = value.Split("-");
                    var leftSide = int.TryParse(parts[0], out leftParse);
                    var rightSide = int.TryParse(parts[1], out leftParse);
                    if (!value.Contains("-") && parts[0].Length>2 && parts[1].Length>3 && leftSide && rightSide)
                    {    
                        context.AddFailure("Podany zły format kodu pocztowego");
                    }
                });
            RuleFor(x => x.Country)
                .NotEmpty()
                .MaximumLength(200);
        }
    }
}
