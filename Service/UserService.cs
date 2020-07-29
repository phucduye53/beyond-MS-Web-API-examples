using System.Collections.Generic;
using System.Linq;
using DemoApi.Models;
using TodoApi.Models;

namespace DemoApi.Service
{
    public interface IUserService
    {
        bool IsValidUser(string Username, string Password);
        User GetLoginUser(string Username, string Password);
        List<string> GetUserRole(User user);
    }
    public class UserService : IUserService
    {
        private readonly TodoContext _context;
        public UserService(TodoContext context)
        {
            _context = context;
        }
        public bool IsValidUser(string Username, string Password)
        {
            return _context.Users.Any(x => x.Username == Username && x.Password == Password);
        }

        public User GetLoginUser(string Username, string Password)
        {
            var user = _context.Users.FirstOrDefault(x => x.Username == Username && x.Password == Password);
            if (user == null)
            {
                return null;
            }
            else
            {
                return user;
            }
        }

        public List<string> GetUserRole(User user)
        {
            var userRole = _context.Roles.Where(p=>p.UserRoles.Any(p=>p.UserId==user.Id)).Select(p=>p.RoleName).ToList();
            return userRole;
        }
    }
}