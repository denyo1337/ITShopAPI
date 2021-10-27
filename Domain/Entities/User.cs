using Domain.Common;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class User : AuditableEntity
    {
        public int Id { get; set; }
        [Required]
        public string Email { get; set; }
        public string NickName { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string SurrName { get; set; }
        public int? PhoneNumber { get; set; }
        public string Nationality { get; set; }
        public string PasswordHash { get; set; }
        public DateTime BirthDay { get; set; }
        public virtual Role Role { get; set; }
        public int RoleId { get; set; }
        public ICollection<Address> Addresses { get; set; }
        public bool IsBanned { get; set; }
        [DefaultValue("true")]
        public bool IsActive { get; set; }

    }
}
