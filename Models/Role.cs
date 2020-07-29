using System.Collections.Generic;

namespace DemoApi.Models
{
    public class Role
    {
        public long Id { get; set; }
        public string RoleName { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}