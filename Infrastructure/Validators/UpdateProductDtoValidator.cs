using Application.DTOs.Enums;
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
    public class UpdateProductDtoValidator : AbstractValidator<UpdateProductDto>
    {
        public UpdateProductDtoValidator(ITShopDbContext dbContext)
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MinimumLength(5)
                .MaximumLength(250);
            RuleFor(x => x.Amount)
            .Must(x => x.Value > 0).WithMessage("Ilość produktu nie może być ujemna");
            RuleFor(x => x.Price)
                .Must(x => x.Value > 0).WithMessage("Wartość produktu nie może być ujemna");
            RuleFor(x => x.Description)
                .MaximumLength(500);
            RuleFor(x => x.ProductType)
                .IsEnumName(typeof(ProductTypeEnum)).WithMessage("Wybrano zły productType, prawidłowe to: Software, Hardware");
        }
    }
}
