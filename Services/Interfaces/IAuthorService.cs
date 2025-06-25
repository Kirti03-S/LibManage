using LibManage.DTOs;

namespace LibManage.Services.Interfaces
{
    public interface IAuthorService
    {
        Task<List<AuthorDto>> GetAllAuthorsAsync();
        Task CreateAuthorAsync(CreateAuthorDto dto);
    }
}
