using API.Database;
using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BuggyController : BaseApiController
    {
        private DatabaseContext context;

        public BuggyController(DatabaseContext context)
        {
            this.context = context;
        }

        [Authorize]
        [HttpGet("unauthorized")]
        public ActionResult<string> GetSecret()
        {
            return "Secret text";
        }

        [HttpGet("not-found")]
        public ActionResult<User> GetNotFound()
        {
            var thing = this.context.Users.Find(-1);

            if(thing == null) return NotFound();

            return Ok(thing);
        }

        [HttpGet("server-error")]
        public ActionResult<string> GetServerError()
        {
            var thing = this.context.Users.Find(-1);

            var thingToReturn = thing.ToString();

            return thingToReturn;
        }

        [HttpGet("bad-request")]
        public ActionResult<string> GetBadRequest()
        {
            return BadRequest("This was a bad request");
        }
    }
}