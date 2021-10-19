using Domain.Common;
using Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Validators
{
    public class ProductsQueryValidator : AbstractValidator<ProductsQuery>
    {
        private int[] allowedPageSizes = new[] { 5, 10, 20, 50 };
        private string[] allowedSortByColumnNames = { nameof(Product.Name), nameof(Product.Amount), nameof(Product.Price)};
        public ProductsQueryValidator()
        {
            RuleFor(x => x.PageNumber)
                .GreaterThanOrEqualTo(1);
            RuleFor(x => x.PageSize)
                .Custom((value, context) =>
                {
                    if (!allowedPageSizes.Contains(value))
                    {
                        context.AddFailure("PageSize", $"PageSize musi zawierać się [{string.Join(",", allowedPageSizes)}]");
                    }
                });
            RuleFor(x => x.SortBy)
                .Must(value => string.IsNullOrEmpty(value) || allowedSortByColumnNames.Contains(value))
                .WithMessage($"SortBy jest opcjonalne lub musi zawierać wartości  {string.Join(",",allowedSortByColumnNames)}");
        }
    }
}
