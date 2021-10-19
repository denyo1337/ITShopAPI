using Domain.Common;
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
        }
    }
}
