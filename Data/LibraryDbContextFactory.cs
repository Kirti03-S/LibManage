//using LibManage.Models;
//using Microsoft.EntityFrameworkCore;

//namespace LibManage.Data
//{
//    public class LibraryDbContext : DbContext
//    {
//        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options) { }

//        public DbSet<Book> Books => Set<Book>();
//        public DbSet<Genre> Genres => Set<Genre>();
//        public DbSet<Author> Authors => Set<Author>();
//        public DbSet<Review> Reviews => Set<Review>();
//        public DbSet<LendingRecord> LendingRecords => Set<LendingRecord>();

//        protected override void OnModelCreating(ModelBuilder modelBuilder)
//        {
//            base.OnModelCreating(modelBuilder);

//            // Seed Genres
//            modelBuilder.Entity<Genre>().HasData(
//                new Genre { Id = 1, Name = "Science Fiction" },
//                new Genre { Id = 2, Name = "Fantasy" }
//            );

//            // Seed Authors
//            modelBuilder.Entity<Author>().HasData(
//                new Author { Id = 1, FirstName = "Isaac Asimov" },
//                new Author { Id = 2, FirstName = "J.K. Rowling" }
//            );

//            // Seed Books
//            modelBuilder.Entity<Book>().HasData(
//                new Book
//                {
//                    Id = 1,
//                    Title = "Foundation",
//                    ISBN = "978-0-553-80371-0",
//                    PublishedDate = new DateTime(1951, 6, 1),
//                    Stock = 5,
//                    GenreId = 1,
//                    AuthorId = 1
//                },
//                new Book
//                {
//                    Id = 2,
//                    Title = "Harry Potter and the Sorcerer's Stone",
//                    ISBN = "978-0-7475-3269-9",
//                    PublishedDate = new DateTime(1997, 6, 26),
//                    Stock = 10,
//                    GenreId = 2,
//                    AuthorId = 2
//                }
//            );
//        }
//    }
//}
