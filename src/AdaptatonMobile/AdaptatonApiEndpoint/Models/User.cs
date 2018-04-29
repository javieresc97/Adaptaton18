using System;
namespace AdaptatonApiEndpoint.Models
{
    public class User
    {        
        public User()
        {
        }
    }

    public class UserRegister
    {
        public string Id { get; set; }
        public string DNI { get; set; }
        public string Password { get; set; }
        public bool Disability { get; set; }
        public bool Group { get; set; }
        public string Phone { get; set; }
        public string Cellphone { get; set; }
    }
}
