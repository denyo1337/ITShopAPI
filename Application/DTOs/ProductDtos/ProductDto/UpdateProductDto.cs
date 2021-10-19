using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.ProductDtos.ProductDto
{
    public class UpdateProductDto
    {
        public string Name { get; set; }
        public string ProductType { get; set; }
        public double? Price { get; set; }
        public int? Amount { get; set; }
        public string Description { get; set; }
    }
}
