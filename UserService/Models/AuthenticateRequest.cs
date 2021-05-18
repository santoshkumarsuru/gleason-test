using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class AuthenticateRequest
    {
        [Required]
        public string Username { get; set; }

        private string _password;
        [Required]
        public string Password
        {
            get
            {
                return _password;
            }

            set
            {
                byte[] data = System.Text.Encoding.ASCII.GetBytes(value);
                data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data);
                _password = System.Text.Encoding.ASCII.GetString(data);
            }
        }
    }
}