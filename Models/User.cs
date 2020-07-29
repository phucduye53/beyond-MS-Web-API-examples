using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DemoApi.Models
{
    public class User
    {
        public long Id { get; set; }

        [Required]
        public string Username { get; set; }
        public string Password { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}