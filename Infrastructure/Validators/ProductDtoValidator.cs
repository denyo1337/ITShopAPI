using Application.DTOs.ProductDtos.ProductDto;
using FluentValidation;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Validators
{
    public class ProductDtoValidator : AbstractValidator<ProductDto>
    {
        public ProductDtoValidator(ITShopDbContext dbContext)
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(250)
                .Custom((value, context) =>
                {
                    var isTaken = dbContext.Products.Any(x => x.Name == value);
                    if (isTaken)
                    {
                        context.AddFailure($"Nazwa product '{value}' już istnieje, powinieneś zaaktualizować istniejący produkt");
                    }
                });
            RuleFor(x => x.Amount)
                .Must(x => x.Value < 0).WithMessage("Ilość produktu nie może być ujemna");
            RuleFor(x => x.Price)
                .Must(x => x.Value < 0).WithMessage("Wartość produktu nie może być ujemna");
            RuleFor(x => x.Description)
                .MaximumLength(500);

        }
    }
}
