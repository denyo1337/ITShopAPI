using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class ProfileDetailsDto
    {
        public string NickName { get; set; }
        public string Name { get; set; }
        public string SurrName { get; set; }
        public int? PhoneNumber { get; set; }
        public string Nationality { get; set; }
        public DateTime? BirthDay { get; set; }
    }
}
