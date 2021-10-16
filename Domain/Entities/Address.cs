using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Address : AuditableEntity
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public string Towm { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public int UserId { get; set; }
    }
}
