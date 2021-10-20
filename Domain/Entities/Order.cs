using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId {  get; set; }
        public int AddressId { get; set; }
        public Address Address { get; set; }
        public DateTime DateOrdered { get; set; }
        public decimal TotalCost { get; set; }
        public List<OrderAmountProducts> OrderAmountProducts { get; set; }
    }
}
