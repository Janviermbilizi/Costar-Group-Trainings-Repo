using API.DTOs;
using API.Interfaces;
using API.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Database
{
    public class UserRepository : IUserRepository
    {
        private readonly DatabaseContext context;
        private readonly IMapper mapper;
        public UserRepository(DatabaseContext context, IMapper mapper)
        {
            this.mapper = mapper;
            this.context = context;

        }

        public async Task<MemberDto> GetMemberAsync(string memberId)
        {
            return await this.context.Users
                    .Where(x => x.MemberId == memberId)
                    .ProjectTo<MemberDto>(this.mapper.ConfigurationProvider)
                    .SingleOrDefaultAsync();
        }

        public Task GetMemberAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<MemberDto>> GetMembersAsync()
        {
            return await this.context.Users
                .ProjectTo<MemberDto>(this.mapper.ConfigurationProvider)
                .ToListAsync();
        }
        
        public async Task<User> GetUserByIdAsync(int id)
        {
            return await this.context.Users.FindAsync(id);
        }

        public async Task<User> GetUserByMemberIdAsync(string memberId)
        {
            return await this.context.Users
            .Include(p => p.Photos)
            .SingleOrDefaultAsync(x => x.MemberId == memberId);
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await this.context.Users
            .Include(p => p.Photos)
            .ToListAsync();
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