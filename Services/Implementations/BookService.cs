using LibManage.Data;
using LibManage.DTOs;         
using LibManage.DTOs.Book;      
using LibManage.Models;
using LibManage.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

public class BookService : IBookService
{
    private readonly LibraryDbContext _context;

    public BookService(LibraryDbContext context)
    {
        _context = context;
    }

    public async Task<List<BookDto>> GetAllBooksAsync()
    {
        return await _context.Books
            .Include(b => b.Author)
            .Include(b => b.Genre)
            .Select(b => new BookDto
            {
                Id = b.Id,
                Title = b.Title,
                ISBN = b.ISBN,
                PublishedDate = b.PublishedDate,
                Stock = b.Stock,
                Price = b.Price,
                AuthorId = b.AuthorId,
                AuthorName = b.Author.FirstName,
                GenreId = b.GenreId,
                GenreName = b.Genre.Name,
                CoverImageUrl = b.CoverImageUrl
            })
            .ToListAsync();
    }

    public async Task<BookDto> GetBookByIdAsync(int id)
    {
        var book = await _context.Books
            .Include(b => b.Author)
            .Include(b => b.Genre)
            .FirstOrDefaultAsync(b => b.Id == id);

        if (book == null) return null;

        return new BookDto
        {
            Id = book.Id,
            Title = book.Title,
            ISBN = book.ISBN,
            PublishedDate = book.PublishedDate,
            Stock = book.Stock,
            Price = book.Price,
            AuthorId = book.AuthorId,
            AuthorName = book.Author.FirstName,
            GenreId = book.GenreId,
            GenreName = book.Genre.Name,
            CoverImageUrl = book.CoverImageUrl
        };
    }

    public async Task<BookDto> CreateBookAsync(CreateBookDto bookDto)
    {
        var book = new Book
        {
            Title = bookDto.Title,
            ISBN = bookDto.ISBN,
            PublishedDate = bookDto.PublishedDate,
            Price = bookDto.Price,
            Stock = bookDto.Stock,
            GenreId = bookDto.GenreId,
            AuthorId = bookDto.AuthorId,
            CoverImageUrl = bookDto.CoverImageUrl
        };

        _context.Books.Add(book);
        await _context.SaveChangesAsync();

        var author = await _context.Authors.FindAsync(book.AuthorId);
        var genre = await _context.Genres.FindAsync(book.GenreId);

        return new BookDto
        {
            Id = book.Id,
            Title = book.Title,
            ISBN = book.ISBN,
            PublishedDate = book.PublishedDate,
            Stock = book.Stock,
            Price = book.Price,
            AuthorId = book.AuthorId,
            AuthorName = $"{author?.FirstName} {author?.LastName}",
            GenreId = book.GenreId,
            GenreName = genre?.Name ?? "",
            CoverImageUrl = book.CoverImageUrl
        };
    }

    public async Task<BookDto> UpdateBookAsync(int id, CreateBookDto bookDto)
    {
        var book = await _context.Books.FindAsync(id);
        if (book == null) return null;

        book.Title = bookDto.Title;
        book.ISBN = bookDto.ISBN;
        book.PublishedDate = bookDto.PublishedDate;
        book.Stock = bookDto.Stock;
        book.Price = bookDto.Price;
        book.GenreId = bookDto.GenreId;
        book.AuthorId = bookDto.AuthorId;
        book.CoverImageUrl = bookDto.CoverImageUrl;

        await _context.SaveChangesAsync();

        var author = await _context.Authors.FindAsync(book.AuthorId);
        var genre = await _context.Genres.FindAsync(book.GenreId);

        return new BookDto
        {
            Id = book.Id,
            Title = book.Title,
            ISBN = book.ISBN,
            PublishedDate = book.PublishedDate,
            Stock = book.Stock,
            Price = book.Price,
            AuthorId = book.AuthorId,
            AuthorName = $"{author?.FirstName} {author?.LastName}",
            GenreId = book.GenreId,
            GenreName = genre?.Name ?? "",
            CoverImageUrl = book.CoverImageUrl

        };
    }

    public async Task<bool> DeleteBookAsync(int id)
    {
        var book = await _context.Books.FindAsync(id);
        if (book == null) return false;

        _context.Books.Remove(book);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<(List<BookDto> Books, int TotalCount)> GetPagedBooksAsync(int pageNumber, int pageSize)
    {
        var query = _context.Books
            .Include(b => b.Author)
            .Include(b => b.Genre)
            .AsQueryable();

        var totalCount = await query.CountAsync();

        var books = await query
            .OrderBy(b => b.Title)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(b => new BookDto
            {
                Id = b.Id,
                Title = b.Title,
                ISBN = b.ISBN,
                PublishedDate = b.PublishedDate,
                Stock = b.Stock,
                Price = b.Price,
                AuthorId = b.AuthorId,
                AuthorName = $"{b.Author.FirstName} {b.Author.LastName}",
                GenreId = b.GenreId,
                GenreName = b.Genre.Name,
                CoverImageUrl = b.CoverImageUrl
            })
            .ToListAsync();

        return (books, totalCount);
    }

    
}
