using LibManage.Models;
namespace LibManage.Repositories.Interfaces
{
    public interface IMemberRepository
    {
        Task<List<Member>> GetAllAsync();
        Task<Member?> GetByIdAsync(int id);
        Task AddAsync(Member member);
        Task UpdateAsync(Member member);
        Task DeleteAsync(Member member);
        Task<Member?> GetByEmailAsync(string email);
        Task<Member?> GetByUsernameAsync(string username);
    }
}
