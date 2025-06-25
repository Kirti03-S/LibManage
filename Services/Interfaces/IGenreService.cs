using LibManage.DTOs;

namespace LibManage.Services.Interfaces
{
    public interface IGenreService
    {
        Task<List<GenreDto>> GetAllGenresAsync();
        Task CreateGenreAsync(CreateGenreDto dto);
    }
}
