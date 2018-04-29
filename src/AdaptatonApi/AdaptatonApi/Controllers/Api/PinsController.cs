using AdaptatonApi.Controllers.Helpers;
using AdaptatonApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace AdaptatonApi.Controllers.Api
{
    [RoutePrefix("api/Pin")]
    public class PinsController : ApiController
    {
        private AdaptatonContext db = new AdaptatonContext();

        [ResponseType(typeof(Incident))]
        public async Task<IHttpActionResult> PostPin(Incident incident)
        {
            if (incident == null) return BadRequest();
            var load = new BlobStorage();
            try
            {
                var photo = await load.UploadImageToStorage(incident.Photo);
                var query = db.CREATE_INCIDENT_SP(incident.UserId, incident.Name, incident.Latitude, incident.Longitude, incident.Secure, photo, incident.Description, incident.Help);

                return Ok();
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
        }

      
      
        [HttpGet]
        public IHttpActionResult GetPins(double lat = 0, double lon = 0, double radius = 100)
        {

            if (lat == 0 || lon == 0)
            {
                var pins = from p in db.Pins.ToList()
                          select new PinGet
                          {
                              Id = p.Id,
                              Latitude = p.Latitude,
                              Longitude = p.Longitude
                          };
                return Ok(pins);
            }
            else
            {
                var pins = from p in db.Pins.ToList()
                           select new PinGet
                           {
                               Id = p.Id,
                               Latitude = p.Latitude,
                               Longitude = p.Longitude
                           };

                var pinList = new List<PinGet>();

                foreach (var pin in pins)
                {
                    double distance = Helpers.CoordinatesConverter.DistanceInMiles(
                                Convert.ToDouble(pin.Latitude),
                                Convert.ToDouble(pin.Longitude),
                                lat, lon);
                    if (distance < radius)
                        pinList.Add(pin);
                }
                return Ok(pinList);
            }
        }
    }
}
