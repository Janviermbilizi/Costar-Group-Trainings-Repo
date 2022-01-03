using API.Database;
using API.DTOs;
//user model
using API.Models;
//ActionResult
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
//hash password
using System.Security.Cryptography;
//Encoding
using System.Text;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly DatabaseContext context;
        public AccountController(DatabaseContext context)
        {
            this.context = context;
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(RegisterDto registerDto)
        {
            if(await UserExists(registerDto.Email)) return BadRequest("Email is taken, please sign in or Reset Password");
            
            using var hmac = new HMACSHA512();

            var newUser = new User
            {
                FullName = registerDto.FullName,
                Email = registerDto.Email.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                PasswordSalt = hmac.Key
            };

            this.context.Users.Add(newUser);

            await this.context.SaveChangesAsync();

            return newUser;
        }

        private async Task<bool> UserExists(string email)
        {
            return await this.context.Users.AnyAsync(user => user.Email == email.ToLower());
        }
    }
}