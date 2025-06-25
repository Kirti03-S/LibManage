using LibManage.DTOs;           // For BookDto
using LibManage.DTOs.Book;      // For CreateBookDto

namespace LibManage.Services.Interfaces
{
    public interface IBookService
    {
        Task<List<BookDto>> GetAllBooksAsync();
        Task<BookDto> GetBookByIdAsync(int id);
        Task<BookDto> CreateBookAsync(CreateBookDto dto);
        Task<BookDto> UpdateBookAsync(int id, CreateBookDto dto);
        Task<bool> DeleteBookAsync(int id);
        Task<(List<BookDto> Books, int TotalCount)> GetPagedBooksAsync(int pageNumber, int pageSize);
       

    }
}
