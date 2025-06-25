using LibManage.Data;
using LibManage.DTOs;
using LibManage.Models;
using LibManage.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;

namespace LibManage.Services
{
    public class GenreService : IGenreService
    {
        private readonly LibraryDbContext _context;

        public GenreService(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<List<GenreDto>> GetAllGenresAsync()
        {
            return await _context.Genres
                .Select(g => new GenreDto
                {
                    Id = g.Id,
                    Name = g.Name
                }).ToListAsync();
        }

        public async Task CreateGenreAsync(CreateGenreDto dto)
        {
            var genre = new Genre
            {
                Name = dto.Name
            };

            _context.Genres.Add(genre);
            await _context.SaveChangesAsync();
        }
    }
}
