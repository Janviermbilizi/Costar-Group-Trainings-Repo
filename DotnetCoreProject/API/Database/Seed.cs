using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Database
{
    public class Seed
    {
        public static async Task SeedUsers (DatabaseContext context)
        {

            if (await context.Users.AnyAsync()) return;
        
            var userData = await System.IO.File.ReadAllTextAsync("Seeds/UserSeedData.json");

            var users = JsonSerializer.Deserialize<List<User>>(userData);

            Boolean PublicIdTaken(string memberID)
            {
                return context.Users.Any(user => user.MemberId == memberID.ToLower());
            };
            
            foreach (var user in users)
            {
                using var hmac = new HMACSHA512();

                user.Email = user.Email.ToLower();

                var option1 = user.FullName.Replace(' ', '-');
                var option2 = user.FullName.Replace(' ', '.');

                if(!PublicIdTaken(option1)) {
                    user.MemberId = option1.ToLower();
                } else if(!PublicIdTaken(option2)){
                    user.MemberId = option2.ToLower();
                }

                user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("P@$$w0rd"));
                user.PasswordSalt = hmac.Key;
                context.Users.Add(user);
            }

            await context.SaveChangesAsync();
        }
    }
}