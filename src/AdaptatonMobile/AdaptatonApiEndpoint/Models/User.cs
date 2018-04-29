using System;
namespace AdaptatonApiEndpoint.Models
{
    public class User
    {        
        public string Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Dni { get; set; }
        public bool Group { get; set; }
        public bool Disability { get; set; }
        public string Phone { get; set; }
        public string Cellphone { get; set; }
    }

    public class UserRegister
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Dni { get; set; }
        public string Password { get; set; }
        public bool Group { get; set; }
        public bool Disability { get; set; }
        public string Phone { get; set; }
        public string Cellphone { get; set; }
    }
}
