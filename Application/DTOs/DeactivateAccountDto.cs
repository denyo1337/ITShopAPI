using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class DeactivateAccountDto
    {
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
