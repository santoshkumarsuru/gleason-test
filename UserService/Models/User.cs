
using System.Collections.Generic;

namespace UserService.Models
{
    public class User
    {
        public int UserId { set; get; }
        public string Email { set; get; }
        public string FirstName { set; get; }
        public string LastName { set; get; }
        public string UserName { set; get; }
        public string CustomerName { set; get; }
        public bool isTrailUser { set; get; }
        public ICollection<Role> Roles { get; set; }

    }
}
