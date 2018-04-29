using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdaptatonApi.Models
{
    public class Incident
    {
        //Pin
        public string UserId { get; set; }
        public string Name { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public bool Secure { get; set; }
        //Alert
        public byte[] Photo { get; set; }
        public string Description { get; set; }
        public bool Help { get; set; }
    }

    public class PinGet
    {
        public string Id { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
    }
}