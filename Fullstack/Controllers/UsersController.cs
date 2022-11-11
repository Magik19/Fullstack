using Fullstack.Context;
using Fullstack.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Fullstack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly AppDbContext context;
        public UsersController(AppDbContext context)
        {
            this.context = context;
        }

        // GET: api/<UsersController>
        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                return Ok(context.Users_db1.ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}", Name = "Getuser")]
        public ActionResult Get(int id)
        {
            try
            {
                var user = context.Users_db1.FirstOrDefault(u => u.id == id);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/<UsersController>
        [HttpPost]
        public ActionResult Post([FromBody] users_db1 user)
        {
            try
            {
                context.Users_db1.Add(user);
                context.SaveChanges();
                return CreatedAtRoute("GetUser", new { id = user.id }, user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] users_db1 user)
        {
            try
            {
                if (user.id == id)
                {
                    context.Entry(user).State = EntityState.Modified;
                    context.SaveChanges();
                    return CreatedAtRoute("GetUser", new { id = user.id }, user);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var user = context.Users_db1.FirstOrDefault(u => u.id == id);
                if (user != null)
                {
                    context.Users_db1.Remove(user);
                    context.SaveChanges();
                    return Ok(id);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}