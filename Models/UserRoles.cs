using System.ComponentModel.DataAnnotations.Schema;

namespace DemoApi.Models
{
    public class UserRole
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        public long RoleId { get; set; }
        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }
    }
}