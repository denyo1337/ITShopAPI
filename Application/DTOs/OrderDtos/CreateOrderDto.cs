using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.OrderDtos
{
    public class CreateOrderDto
    {
        public List<ProductForOrderDto> Products { get; set; }
        public int AddressId { get; set; }

    }
}
