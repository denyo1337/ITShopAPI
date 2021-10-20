using Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Product : AuditableEntity
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string ProductType { get; set; }
        public double? Price { get; set; }
        public int? Amount { get; set; }
        public string Description { get; set; }
        public ICollection<Image> Images { get; set; }
    }
}
