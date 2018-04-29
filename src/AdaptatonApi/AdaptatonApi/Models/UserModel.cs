using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdaptatonApi.Models
{
    public class Credential
    {
        public string Dni { get; set; }
        public string Password { get; set; }
    }

    public class UserGet
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
}