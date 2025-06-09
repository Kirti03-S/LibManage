using LibManage.Data;
using LibManage.DTOs.Book;
using LibManage.Models;
using LibManage.Services.Interfaces;
using LibManage.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LibManage.Controllers
{
    [Authorize]
    public class BookController : Controller
    {
        private readonly IBookService _bookService;
        private readonly LibraryDbContext _context;

        public BookController(IBookService bookService, LibraryDbContext context)
        {
            _bookService = bookService;
            _context = context;
        }

        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> Index()
        {
            var books = await _bookService.GetAllBooksAsync();

            var viewModel = books.Select(b => new BookViewModel
            {
                Id = b.Id,
                Title = b.Title,
                ISBN = b.ISBN,
                PublishedDate = b.PublishedDate,
                Stock = b.Stock,
                Price=b.Price,
                GenreId = b.GenreId,
                Genre = new Genre { Id = b.GenreId, Name = b.GenreName },
                AuthorId = b.AuthorId,
                Author = new Author { Id = b.AuthorId, FirstName = b.AuthorName }
            });

            return View(viewModel);
        }

        public IActionResult Create()
        {
            ViewBag.Authors = new SelectList(_context.Authors.ToList(), "Id", "FirstName");
            ViewBag.Genres = new SelectList(_context.Genres.ToList(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(BookViewModel vm)
        {
            ViewBag.Authors = new SelectList(_context.Authors.ToList(), "Id", "FirstName");
            ViewBag.Genres = new SelectList(_context.Genres.ToList(), "Id", "Name");

            if (!ModelState.IsValid)
            {
                // Log all validation errors
                foreach (var entry in ModelState)
                {
                    var key = entry.Key;
                    var errors = entry.Value.Errors;

                    foreach (var error in errors)
                    {
                        Console.WriteLine($"Validation error on '{key}': {error.ErrorMessage}");
                    }
                }

                return View(vm);
            }


            if (vm.AuthorId == null || vm.GenreId == null)
            {
                ModelState.AddModelError("", "Author and Genre are required.");
                return View(vm);
            }

            await _bookService.CreateBookAsync(new CreateBookDto
            {
                Title = vm.Title,
                ISBN = vm.ISBN,
                PublishedDate = vm.PublishedDate,
                Stock = vm.Stock,
                Price=vm.Price,
                GenreId = vm.GenreId.Value,
                AuthorId = vm.AuthorId.Value,
                CoverImageUrl = vm.CoverImageUrl
            });

            return RedirectToAction(nameof(Index));
        }

        // GET: Book/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            if (book == null) return NotFound();

            var viewModel = new BookViewModel
            {
                Id = book.Id,
                Title = book.Title,
                ISBN = book.ISBN,
                PublishedDate = book.PublishedDate,
                Stock = book.Stock,
                Price=book.Price,
                GenreId = book.GenreId,
                AuthorId = book.AuthorId,
                CoverImageUrl = book.CoverImageUrl
            };

            ViewBag.Authors = new SelectList(_context.Authors.ToList(), "Id", "FirstName");
            ViewBag.Genres = new SelectList(_context.Genres.ToList(), "Id", "Name");

            return View(viewModel);
        }

        // POST: Book/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(int id, BookViewModel vm)
        {
            ViewBag.Authors = new SelectList(_context.Authors.ToList(), "Id", "FirstName");
            ViewBag.Genres = new SelectList(_context.Genres.ToList(), "Id", "Name");

            if (id != vm.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            await _bookService.UpdateBookAsync(vm.Id, new CreateBookDto
            {
                Title = vm.Title,
                ISBN = vm.ISBN,
                PublishedDate = vm.PublishedDate,
                Stock = vm.Stock,
                Price=vm.Price,
                GenreId = vm.GenreId.Value,
                AuthorId = vm.AuthorId.Value,
                CoverImageUrl = vm.CoverImageUrl 
            });

            return RedirectToAction(nameof(Index));
        }

        // GET: Book/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            if (book == null) return NotFound();

            var viewModel = new BookViewModel
            {
                Id = book.Id,
                Title = book.Title,
                ISBN = book.ISBN,
                PublishedDate = book.PublishedDate,
                Stock = book.Stock,
                Price = book.Price,
                GenreId = book.GenreId,
                AuthorId = book.AuthorId
            };

            return View(viewModel);
        }

        // POST: Book/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _bookService.DeleteBookAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            if (book == null) return NotFound();

            var viewModel = new BookViewModel
            {
                Id = book.Id,
                Title = book.Title,
                ISBN = book.ISBN,
                PublishedDate = book.PublishedDate,
                Stock = book.Stock,
                Price = book.Price,
                GenreId = book.GenreId,
                Genre = new Genre { Id = book.GenreId, Name = book.GenreName },
                AuthorId = book.AuthorId,
                Author = new Author { Id = book.AuthorId, FirstName = book.AuthorName }
            };

            return View(viewModel);
        }

        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> Catalog()
        {
            var books = await _bookService.GetAllBooksAsync();

            var viewModel = books.Select(b => new BookViewModel
            {
                Id = b.Id,
                Title = b.Title,
                ISBN = b.ISBN,
                PublishedDate = b.PublishedDate,
                Stock = b.Stock,
                Price = b.Price, // Make sure your Book entity has Price
                CoverImageUrl = b.CoverImageUrl, // Image URL or path
                GenreId = b.GenreId,
                Genre = new Genre { Id = b.GenreId, Name = b.GenreName },
                AuthorId = b.AuthorId,
                Author = new Author { Id = b.AuthorId, FirstName = b.AuthorName }
            }).ToList();

            return View(viewModel);
        }




    }
}
