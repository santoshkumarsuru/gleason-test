using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.DBContext;
using UserService.Models;

namespace UserService.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly ILogger<UserRepository> _logger;

        public UserRepository(ILogger<UserRepository> logger)
        {
            _logger = logger;
        }

        public void AddUser(User user)
        {
            using (var dataContext = new SampleDBContext())
            {
               dataContext.SaveChanges();
               dataContext.Users.Add(user);
               dataContext.SaveChanges();
            }
        }

        public List<User> GetUsers()
        {
            using (var dataContext = new SampleDBContext())
            {
                return dataContext.Users.Include(u => u.Roles).ToList();
            }
        }

    }
}
