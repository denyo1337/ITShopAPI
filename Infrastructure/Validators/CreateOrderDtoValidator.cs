using Application.DTOs.OrderDtos;
using FluentValidation;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Validators
{
    public class CreateOrderDtoValidator : AbstractValidator<CreateOrderDto>
    {
        public CreateOrderDtoValidator(ITShopDbContext dbContext )
        {
            RuleFor(x => x.Products)
                .NotEmpty()
                .ForEach(x => x.Must(x => x.Amount > 0 ));
            RuleFor(x => x.AddressId)
                .Custom((value, context) =>
                {
                    var result = dbContext.Addresses.FirstOrDefault(x => x.Id == value);
                    if (result == null)
                        context.AddFailure("Address", $"Adres o id {value} nie istnieje");
                });

        }
    }
}
