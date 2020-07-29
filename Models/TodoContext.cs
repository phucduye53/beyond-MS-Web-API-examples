using DemoApi.Models;
using Microsoft.EntityFrameworkCore;

namespace TodoApi.Models
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options)
            : base(options)
        {
        }
        public TodoContext()
        {

        }
        public DbSet<TodoItem> TodoItems { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TodoItem>().HasData(new TodoItem()
            {
                Id = 1,
                Name = "feed dog",
                IsComplete = true,
                Secret = "test"
            });
            modelBuilder.Entity<TodoItem>().HasData(new TodoItem()
            {
                Id = 2,
                Name = "walk dog",
                IsComplete = false,
                Secret = "test"
            });
            modelBuilder.Entity<TodoItem>().HasData(new TodoItem()
            {
                Id = 3,
                Name = "feed fish",
                IsComplete = true,
                Secret = "test"
            });
            modelBuilder.Entity<User>().HasData(new User()
            {
                Id = 1,
                Username = "admin",
                Password = "admin"
            },
            new User()
            {
                Id = 2,
                Username = "user",
                Password = "user"
            }
            );
            modelBuilder.Entity<Role>().HasData(new Role()
            {
                Id = 1,
                RoleName = "Master"
            },
            new Role()
            {
                Id = 2,
                RoleName = "Slave"
            }
            );
            modelBuilder.Entity<UserRole>().HasData(new UserRole()
            {
                Id = 1,
                UserId = 1,
                RoleId = 1
            },
            new UserRole()
            {
                Id = 2,
                UserId = 2,
                RoleId = 2
            }
            );
        }
    }
}