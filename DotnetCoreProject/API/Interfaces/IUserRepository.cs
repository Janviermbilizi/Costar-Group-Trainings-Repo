using API.DTOs;
using API.Models;

namespace API.Interfaces
{
    public interface IUserRepository
    {
         void Update(User user);
         Task<bool> SaveAllAsync();
         Task<IEnumerable<User>> GetUsersAsync();
         Task<User> GetUserByIdAsync(int id);
         Task<User> GetUserByMemberIdAsync(string memberId);
        Task GetMemberAsync();
        Task<MemberDto> GetMemberAsync(string memberId);
        Task<IEnumerable<MemberDto>> GetMembersAsync();
    }
}