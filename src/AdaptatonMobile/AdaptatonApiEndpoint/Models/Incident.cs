using System;
using Newtonsoft.Json;

namespace AdaptatonApiEndpoint.Models
{
    public class IncidentRegister
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public bool Secure { get; set; }
        public byte[] Photo { get; set; }
        public string Description { get; set; }
        public bool Help { get; set; }
    }

    public class Incident
    {
        public string Id { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public bool Secure { get; set; }
    }
}
