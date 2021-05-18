using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using UserService.DBContext;
using WebApi.Entities;
using WebApi.Helpers;
using WebApi.Models;

namespace WebApi.Services
{
    public interface IUserRepository
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
        IEnumerable<User> GetAll();
        User GetById(int id);
        void AddUser(User user);

        void Update(User user);
        void Delete(User user);
    }

    public class UserRepository : IUserRepository
    {

        private readonly AppSettings _appSettings;

        public UserRepository(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            List<User> _users = new List<User>();
            using (var dataContext = new SampleDBContext())
            {
                _users = dataContext.Users.Include(u => u.Roles).ToList();
            }
            var user = _users.SingleOrDefault(x => x.Username == model.Username && x.Password == model.Password);

            // return null if user not found
            if (user == null) return null;

            // authentication successful so generate jwt token
            var token = generateJwtToken(user);

            return new AuthenticateResponse(user, token);
        }

        public void AddUser(User user)
        {
            user.Password = "test"; //Default password for all the users
            using (var dataContext = new SampleDBContext())
            {
                dataContext.Users.Add(user);
                dataContext.SaveChanges();
            }
        }

        public void Update(User user)
        {

            using (var dataContext = new SampleDBContext())
            {
                dataContext.Roles.RemoveRange(dataContext.Roles.Where(x => x.UserId == user.UserId));
                dataContext.SaveChanges();
                if (user.Password == null)
                {
                    var userDB = GetById(user.UserId);
                    user.Password = userDB.Password;
                }
                dataContext.Users.Update(user);
                dataContext.SaveChanges();
            }
        }

        public void Delete(User user)
        {

            using (var dataContext = new SampleDBContext())
            {
                dataContext.Roles.RemoveRange(dataContext.Roles.Where(x => x.UserId == user.UserId));
                dataContext.SaveChanges();
                dataContext.Entry(user).State = EntityState.Deleted;
                dataContext.SaveChanges();
            }
        }

        public IEnumerable<User> GetAll()
        {
            List<User> _users = new List<User>();
            using (var dataContext = new SampleDBContext())
            {
                _users = dataContext.Users.Include(u => u.Roles).ToList();
            }
            return _users;
        }

        public User GetById(int id)
        {
            List<User> _users = new List<User>();
            using (var dataContext = new SampleDBContext())
            {
                _users = dataContext.Users.Include(u => u.Roles).ToList();
            }
            return _users.FirstOrDefault(x => x.UserId == id);
        }

        // helper methods

        private string generateJwtToken(User user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.UserId.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}