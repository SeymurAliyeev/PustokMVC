using Microsoft.EntityFrameworkCore;
using PustokMVC.Business.Interfaces;
using PustokMVC.Models;
using PustokMVC.CustomExceptions.GenreExceptions;
using System.Linq.Expressions;
using PustokMVC.Data;

namespace Pustok_BookShopMVC.Business.Implementations
{
    public class GenreService : IGenreService
    {
        private readonly PustokDbContext _context;

        public GenreService(PustokDbContext context)
        {
            _context = context;
        }
        public async Task CreateAsync(Genre genre)
        {
            if (await _context.Genres.AnyAsync(g => g.Name.ToLower() == genre.Name.ToLower()))
                throw new GenreNameAlreadyExistException("Name", "Genre name is already exist!");
            genre.CreatedDate = DateTime.UtcNow.AddHours(4);
            genre.ModifiedDate = DateTime.UtcNow.AddHours(4);
            await _context.Genres.AddAsync(genre);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Genre genre)
        {
            var existGenre = await _context.Genres.FindAsync(genre.Id);
            if (existGenre is null) throw new GenreNotFoundException("Genre not found!");
            if (_context.Genres.Any(g => g.Name.ToLower() == genre.Name.ToLower()) &&
                existGenre.Name.ToLower() != genre.Name.ToLower())
                throw new GenreNameAlreadyExistException("Name", $"Genre with name {existGenre.Name} is already exist!");
            existGenre.Name = genre.Name;
            existGenre.ModifiedDate = DateTime.Now;
            await _context.SaveChangesAsync();
        }
        public async Task<Genre> GetByIdAsync(int id)
        {
            var genre = await _context.Genres.FindAsync(id);
            if (genre is null) throw new GenreNotFoundException("Genre not found!");
            return genre;
        }
        public async Task<List<Genre>> GetAllAsync(Expression<Func<Genre, bool>>? expression = null, params string[] includes)
        {
            var query = _context.Genres.AsQueryable();
            query = _getIncludes(query, includes);
            return expression is not null
                ? await query.Where(expression).ToListAsync()
                : await query.ToListAsync();

        }
        public async Task<Genre> GetSingleAsync(Expression<Func<Genre, bool>>? expression = null)
        {
            var query = _context.Genres.AsQueryable();
            return expression is not null
                ? await query.Where(expression).FirstOrDefaultAsync()
                : await query.FirstOrDefaultAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var genre = await _context.Genres.FindAsync(id);
            if (genre is null) throw new GenreNotFoundException("Genre not found!");
            _context.Remove(genre);
            await _context.SaveChangesAsync();
        }
        public async Task SoftDeleteAsync(int id)
        {
            var genre = await _context.Genres.FindAsync(id);
            if (genre is null) throw new GenreNotFoundException("Genre not found!");
            genre.ModifiedDate = DateTime.UtcNow.AddHours(4);
            genre.IsDeleted = !genre.IsDeleted;
        }

        private IQueryable<Genre> _getIncludes(IQueryable<Genre> query, params string[] includes)
        {
            if (includes is not null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            return query;
        }
    }
}