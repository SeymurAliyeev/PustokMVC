using Microsoft.EntityFrameworkCore;
using Pustok_BookShopMVC.Business.Interfaces;
using PustokMVC.CustomExceptions.AuthorExceptions;
using PustokMVC.CustomExceptions.GenreExceptions;
using PustokMVC.Data;
using PustokMVC.Models;
using System.Linq.Expressions;

namespace Pustok_BookShopMVC.Business.Implementations;

public class AuthorService : IAuthorService
{
    private readonly PustokDbContext _context;
    public AuthorService(PustokDbContext context)
    {
        _context = context;
    }
    public async Task CreateAsync(Author author)
    {
        if (await _context.Authors.AnyAsync(a => a.Fullname.ToLower() == author.Fullname.ToLower()))
            throw new AuthorNameAlreadyExistException("Name", "Author name is already exist!");
        author.CreatedDate = DateTime.UtcNow.AddHours(4);
        author.ModifiedDate = DateTime.UtcNow.AddHours(4);
        await _context.Authors.AddAsync(author);
        await _context.SaveChangesAsync();
    }
    public async Task UpdateAsync(Author author)
    {
        var existAuthor = await _context.Authors.FindAsync(author.Id);
        if (existAuthor is null) throw new GenreNotFoundException("Genre not found!");
        if (_context.Authors.Any(a => a.Fullname.ToLower() == author.Fullname.ToLower()) &&
            existAuthor.Fullname.ToLower() != author.Fullname.ToLower())
            throw new GenreNameAlreadyExistException("Name", $"Genre with name {existAuthor.Fullname} is already exist!");
        existAuthor.Fullname = author.Fullname;
        existAuthor.ModifiedDate = DateTime.UtcNow.AddHours(4);
        await _context.SaveChangesAsync();
    }
    public async Task<Author> GetByIdAsync(int id)
    {
        var author = await _context.Authors.FindAsync(id);
        if (author is null) throw new AuthorNotFoundException("Author not found!");
        return author;
    }
    public async Task<List<Author>> GetAllAsync(Expression<Func<Author, bool>>? expression = null, params string[] includes)
    {
        var query = _context.Authors.AsQueryable();
        query = _getIncludes(query, includes);
        return expression is not null
            ? await query.Where(expression).ToListAsync()
            : await query.ToListAsync();

    }
    public async Task<Author> GetSingleAsync(Expression<Func<Author, bool>>? expression = null)
    {
        var query = _context.Authors.AsQueryable();
        return expression is not null
            ? await query.Where(expression).FirstOrDefaultAsync()
            : await query.FirstOrDefaultAsync();
    }
    public async Task DeleteAsync(int id)
    {
        var author = await _context.Authors.FindAsync(id);
        if (author is null) throw new AuthorNotFoundException("Genre not found!");
        _context.Remove(author);
        await _context.SaveChangesAsync();
    }
    public async Task SoftDeleteAsync(int id)
    {
        var author = await _context.Authors.FindAsync(id);
        if (author is null) throw new GenreNotFoundException("Genre not found!");
        author.ModifiedDate = DateTime.UtcNow.AddHours(4);
        author.IsDeleted = !author.IsDeleted;
    }

    private IQueryable<Author> _getIncludes(IQueryable<Author> query, params string[] includes)
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

