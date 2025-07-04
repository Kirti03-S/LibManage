﻿using LibManage.Models;
using LibManage.Models.LibManage.Models;
using Microsoft.EntityFrameworkCore;

namespace LibManage.Data
{
    public class LibraryDbContext : DbContext
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options) { }

        public DbSet<Book> Books { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<LendingRecord> LendingRecords => Set<LendingRecord>();
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<MembershipPlan> MembershipPlans { get; set; }
        public DbSet<MemberBook> MemberBooks { get; set; }
        public DbSet<MembershipRequest> MembershipRequests { get; set; }







        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MemberBook>()
        .HasKey(mb => new { mb.MemberId, mb.BookId });

            modelBuilder.Entity<MemberBook>()
                .HasOne(mb => mb.Member)
                .WithMany(m => m.MemberBooks)
                .HasForeignKey(mb => mb.MemberId);

            modelBuilder.Entity<MemberBook>()
                .HasOne(mb => mb.Book)
                .WithMany(b => b.MemberBooks)
                .HasForeignKey(mb => mb.BookId);

            modelBuilder.Entity<Member>()
                .HasMany(m => m.SelectedBooks)
                .WithMany(); // optional: .WithMany(b => b.Members)


            // Seed Genres
            modelBuilder.Entity<Genre>().HasData(
                new Genre { Id = 1, Name = "Science Fiction" },
                new Genre { Id = 2, Name = "Fantasy" }
            );

            // Seed Authors
            modelBuilder.Entity<Author>().HasData(
                new Author { Id = 1, FirstName = "Isaac", LastName="Asimov" },
                new Author { Id = 2, FirstName = "J.K.", LastName="Rowling" }
            );

            // Seed Books
            modelBuilder.Entity<Book>().HasData(
                new Book
                {
                    Id = 1,
                    Title = "Foundation",
                    ISBN = "978-0-553-80371-0",
                    PublishedDate = new DateTime(1951, 6, 1),
                    Stock = 5,
                    GenreId = 1,
                    AuthorId = 1
                },
                new Book
                {
                    Id = 2,
                    Title = "Harry Potter and the Sorcerer's Stone",
                    ISBN = "978-0-7475-3269-9",
                    PublishedDate = new DateTime(1997, 6, 26),
                    Stock = 10,
                    GenreId = 2,
                    AuthorId = 2
                }
            );
        }
    }
}
