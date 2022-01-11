using API.Database;
using API.DTOs;
using API.Interfaces;
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
        private readonly ITokenService tokenService;
        public AccountController(DatabaseContext context, ITokenService tokenService)
        {
            this.tokenService = tokenService;
            this.context = context;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if (await UserExists(registerDto.Email)) return BadRequest("Email is taken, please sign in or Reset Password");

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

            return new UserDto
            {
                Username = newUser.FullName,
                Token = this.tokenService.CreateToken(newUser)
            };
        }

        private async Task<bool> UserExists(string email)
        {
            return await this.context.Users.AnyAsync(user => user.Email == email.ToLower());
        }

        //Login Route
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var User = await this.context.Users.SingleOrDefaultAsync(user => user.Email == loginDto.Email);

            if (User == null) return Unauthorized("Invalid Email, please register");

            //Verifying mutch Password
            using var hmac = new HMACSHA512(User.PasswordSalt);

            var computerHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            for (var i = 0; i < computerHash.Length; i++)
            {
                if (computerHash[i] != User.PasswordHash[i]) return Unauthorized("Invalid password, please try again or reset your password");
            }

            return new UserDto
            {
                Username = User.FullName,
                Token = this.tokenService.CreateToken(User)
            };
        }
    }
}