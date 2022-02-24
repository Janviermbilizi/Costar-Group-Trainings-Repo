using API.Interfaces;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Database
{
    public class UserRepository : IUserRepository
    {
        private readonly DatabaseContext context;
        public UserRepository(DatabaseContext context)
        {
            this.context = context;

        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await this.context.Users.FindAsync(id);
        }

        public async Task<User> GetUserByMemberIdAsync(string MemberId)
        {
            return await this.context.Users.SingleOrDefaultAsync(x => x.MemberId == MemberId);
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await this.context.Users.ToListAsync();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await this.context.SaveChangesAsync() > 0;
        }

        public void Update(User user)
        {
            this.context.Entry(user).State = EntityState.Modified;
        }
    }
}