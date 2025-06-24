using LibManage.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibManage.Repositories
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAllAsync();
        Task<Book?> GetByIdAsync(Guid id); 
        Task AddAsync(Book book);
        Task UpdateAsync(Book book);
        Task DeleteAsync(Book book);
        Task<List<Book>> GetBooksByIdsAsync(IEnumerable<int> ids);

    }
}
