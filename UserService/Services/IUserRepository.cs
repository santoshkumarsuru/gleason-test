using System.Collections.Generic;
using UserService.Models;

namespace UserService.Services
{
    public interface IUserRepository
    {
        List<User> GetUsers();
        void AddUser(User user);
    }
}
