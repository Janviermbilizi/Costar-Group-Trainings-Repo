using API.Database;
using API.Interfaces;
using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class UsersController : BaseApiController
    {
        private readonly IUserRepository userRepository;
        public UsersController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        //Get all users
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users = await this.userRepository.GetUsersAsync();
            return Ok(users);
        }

        //Get user by ID
        [Authorize]
        [HttpGet("{MemberId}")]
        public async Task<ActionResult<User>> GetUser(string memberid)
        {
            var user = await this.userRepository.GetUserByMemberIdAsync(memberid);
            return user;
        }
    }
}