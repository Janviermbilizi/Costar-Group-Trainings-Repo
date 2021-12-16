using API.Database;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class UsersController : ControllerBase
    {
        private readonly DatabaseContext context;
        public UsersController(DatabaseContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<User>> GetUsers()
        {
            var users = context.Users.ToList();
            
            return users;
        }
        [HttpGet("{id}")]
        public ActionResult<User> GetUser(int id)
        {
            var user = context.Users.Find(id);
            
            return user;
        }
    }
}