using APIDEMO_.Context;
using APIDEMO_.Models;
using APIDEMO_.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APIDEMO_.Controllers
{/// <summary>
 /// Antes de que se pueda acceder al controller tiene que autenticarse
 /// </summary>

    [EnableCors("MyPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private ApiAppContext apiContext;

        public UserController(ApiAppContext context)
        {
            apiContext = context;
            apiContext.Database.EnsureCreated();
        }
        // GET: api/<ValuesController>
        
    
        [HttpGet]
        [Route("getvalues")]
        public ActionResult<IEnumerable<User>> Get()
        {
            return apiContext.Users;
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(string id)
        {
            Guid.TryParse(id, out var userId);
            if (userId != Guid.Empty )
            {
                var userFound = apiContext.Users.FirstOrDefault(p => p.UserId == userId);
                if (userFound != null)
                {
                    return Ok(userFound);
                }
                else
                {
                    return NotFound();
                }

            }
            else
            {
                return BadRequest();
            }
        }

        // POST api/<ValuesController>
        [HttpPost]
        public void Post([FromBody] User value)
        {
            apiContext.Users.Add(value);
            apiContext.SaveChanges();
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(string id, [FromBody] User value)
        {
            Guid.TryParse(id, out var userId);
            if (userId != Guid.Empty)
            {
                var userFound = apiContext.Users.FirstOrDefault(p => p.UserId == userId);
                if (userFound != null)
                {
                    userFound.Name = value.Name;
                    userFound.LastName = value.LastName;
                    userFound.Active = value.Active;
                    apiContext.SaveChanges();
                }
            }
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public async Task Delete(string id)
        {
            Guid.TryParse(id, out var userId);
            if (userId != Guid.Empty)
            {
                var userFound = apiContext.Users.FirstOrDefault(p => p.UserId == userId);
                apiContext.Users.Remove(userFound);
               await apiContext.SaveChangesAsync();
            }
        }



        [HttpGet]
        [Route("active")]
        public ActionResult<IEnumerable<User>> GetUserActive()
        {
            return apiContext.Users.Where(p => p.Active).Include(p => p.UserRoles).ToList();
        }

        [HttpGet]
        [Route("GetRoles")]
        public ActionResult<IEnumerable<UserRole>> GetRoles()
        {
            return apiContext.UserRoles.Include(p=> p.User).ToList();
        }
    }
}
