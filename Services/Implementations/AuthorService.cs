using LibManage.Data;
using LibManage.DTOs;
using LibManage.Models;
using LibManage.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;

namespace LibManage.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly LibraryDbContext _context;

        public AuthorService(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<List<AuthorDto>> GetAllAuthorsAsync()
        {
            return await _context.Authors
                .Select(a => new AuthorDto
                {
                    Id = a.Id,
                    FirstName = a.FirstName,
                    LastName = a.LastName,
                    Biography = a.Biography
                }).ToListAsync();
        }

        public async Task CreateAuthorAsync(CreateAuthorDto dto)
        {
            var author = new Author
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Biography = dto.Biography
            };

            _context.Authors.Add(author);
            await _context.SaveChangesAsync();
        }
    }
}
