using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using AdaptatonApi.Models;

namespace AdaptatonApi.Controllers.Api
{
    [RoutePrefix("api/User")]
    public class UsersController : ApiController
    {
        private AdaptatonContext db = new AdaptatonContext();
        

        // GET: api/Users
        public IQueryable<User> GetUsers()
        {
            return db.Users;
        }

        // GET: api/Users/5
        [ResponseType(typeof(User))]
        public async Task<IHttpActionResult> GetUser(string id)
        {
            User user = await db.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // PUT: api/Users/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutUser(string id, User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.Id)
            {
                return BadRequest();
            }

            db.Entry(user).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Users
        [ResponseType(typeof(User))]
        public async Task<IHttpActionResult> PostUser(User user)
        {

            if (user == null) return BadRequest();
            var userFind = (from u in db.Users.ToList()
                           where u.Dni == user.Dni
                           select new User
                           {
                               Dni = u.Dni
                           }).FirstOrDefault();

            if (userFind == null) 
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                db.Users.Add(user);

                try
                {
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateException)
                {
                    if (UserExists(user.Id))
                    {
                        return Conflict();
                    }
                    else
                    {
                        throw;
                    }
                }

                return CreatedAtRoute("DefaultApi", new { id = user.Id }, user);
            }

            return BadRequest("Dni Exist");
        }

        // DELETE: api/Users/5
        [ResponseType(typeof(User))]
        public async Task<IHttpActionResult> DeleteUser(string id)
        {
            User user = await db.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            db.Users.Remove(user);
            await db.SaveChangesAsync();

            return Ok(user);
        }

        // POST: api/Users/Credential
        [Route("Auth")]
        [HttpPost]
        [ResponseType(typeof(Credential))]
        public async Task<IHttpActionResult> Auth([FromBody] Credential credential)
        {
            if (credential == null) return BadRequest("Parameteres required");


            var auth = await db.Users.Where(u => u.Dni == credential.Dni && u.Password == credential.Password).FirstOrDefaultAsync();

            if (auth == null) return NotFound();

            var user = new UserGet()
            {
                Id = auth.Id,
                Name = auth.Name,
                LastName = auth.LastName,
                Dni = auth.Dni,
                Group = auth.Group,
                Disability = auth.Disability,
                Phone = auth.Phone,
                Cellphone = auth.Cellphone
            };

            return Ok(user);
        }
        [Route("FindByDni/{dni}")]
        [HttpGet]
        public async Task<IHttpActionResult> FindByDni(string dni)
        {
            var userExist = await db.Users.FirstOrDefaultAsync(u => u.Dni == dni);


            if (userExist == null) return NotFound();

            var user = new UserGet()
            {
                Id = userExist.Id,
                Name = userExist.Name,
                LastName = userExist.LastName,
                Dni = userExist.Dni,
                Group = userExist.Group,
                Disability = userExist.Disability,
                Phone = userExist.Phone,
                Cellphone = userExist.Cellphone
            };

            return Ok(user);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserExists(string id)
        {
            return db.Users.Count(e => e.Id == id) > 0;
        }
    }
}