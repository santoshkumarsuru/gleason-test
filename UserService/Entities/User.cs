using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WebApi.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Customer { get; set; }
        public string Email { set; get; }
        public ICollection<Role> Roles { get; set; }

        [JsonIgnore]
        public string Password { set; get; }
        //{
        //    get
        //    {
        //        return _password;
        //    }

        //    set
        //    {
        //        byte[] data = System.Text.Encoding.ASCII.GetBytes(value);
        //        data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data);
        //        _password = System.Text.Encoding.ASCII.GetString(data);
        //    }
        //}


    }
}