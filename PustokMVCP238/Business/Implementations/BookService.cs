
using Microsoft.EntityFrameworkCore;
using PustokMVC.Business.Interfaces;
using PustokMVC.CustomExceptions.BookExceptions;
using PustokMVC.Models;
using PustokMVC.Extensions;
using System.Linq.Expressions;
using PustokMVC.Data;

namespace Pustok_BookShopMVC.Business.Implementations;

public class BookService : IBookService
{
    private readonly PustokDbContext _context;
    private readonly IWebHostEnvironment _env;
    public BookService(PustokDbContext context, IWebHostEnvironment env)
    {
        _context = context;
        _env = env;
    }
    public async Task CreateAsync(Book book)
    {
        BookImage posterImage = new BookImage()
        {
            Book = book,
            ImageUrl = book.PosterImageFile.SaveFile(_env.WebRootPath, "uploads/books"),
            IsPoster = true
        };
        await _context.BookImages.AddAsync(posterImage);

        BookImage hoverImage = new BookImage()
        {
            Book = book,
            ImageUrl = book.HoverImageFile.SaveFile(_env.WebRootPath, "uploads/books"),
            IsPoster = false
        };
        posterImage.CreatedDate = DateTime.UtcNow.AddHours(4);
        posterImage.ModifiedDate = DateTime.UtcNow.AddHours(4);
        hoverImage.CreatedDate = DateTime.UtcNow.AddHours(4);
        hoverImage.ModifiedDate = DateTime.UtcNow.AddHours(4);
        book.CreatedDate = DateTime.UtcNow.AddHours(4);
        book.ModifiedDate = DateTime.UtcNow.AddHours(4);
        await _context.BookImages.AddAsync(hoverImage);
        await _context.Books.AddAsync(book);
        await _context.SaveChangesAsync();
    }
    public async Task UpdateAsync(Book book)
    {
        var existBook = await _context.Books.FindAsync(book.Id);
        if (existBook is null) throw new BookNotFoundException("Book not found");
        BookImage? posterImage = await _context.BookImages
            .Where(pi => pi.BookId == existBook.Id && pi.IsPoster == true)
            .FirstOrDefaultAsync();
        FileManager.DeleteFile(_env.WebRootPath, "uploads/books", posterImage.ImageUrl);
        _context.BookImages.Remove(posterImage);

        BookImage? hoverImage = await _context.BookImages
           .Where(hi => hi.BookId == existBook.Id && hi.IsPoster == false)
           .FirstOrDefaultAsync();
        FileManager.DeleteFile(_env.WebRootPath, "uploads/books", hoverImage.ImageUrl);
        _context.BookImages.Remove(hoverImage);

        List<BookImage>? bookImages = await _context.BookImages
                   .Where(bi => bi.BookId == existBook.Id && bi.IsPoster == null).ToListAsync();
        if (bookImages is not null)
        {
            foreach (var image in bookImages)
            {
                FileManager.DeleteFile(_env.WebRootPath, "uploads/books", image.ImageUrl);
                _context.BookImages.Remove(image);
                await _context.SaveChangesAsync();
            }
        }

        BookImage newPosterImage = new BookImage()
        {
            Book = book,
            ImageUrl = book.PosterImageFile.SaveFile(_env.WebRootPath, "uploads/books"),
            IsPoster = true
        };
        await _context.BookImages.AddAsync(posterImage);

        BookImage newHoverImage = new BookImage()
        {
            Book = book,
            ImageUrl = book.HoverImageFile.SaveFile(_env.WebRootPath, "uploads/books"),
            IsPoster = false
        };
        await _context.BookImages.AddAsync(newHoverImage);

        existBook.Author.Fullname = book.Author.Fullname;
        existBook.Genre.Name = book.Genre.Name;
        existBook.Desc = book.Desc;
        existBook.CostPrice = book.CostPrice;
        existBook.SalePrice = book.SalePrice;
        existBook.DiscountPercent = book.DiscountPercent;
        existBook.BookCode = book.BookCode;
        existBook.ModifiedDate = DateTime.UtcNow.AddHours(4);

        await _context.SaveChangesAsync();
    }
    public async Task<Book> GetByIdAsync(int id)
    {
        var book = await _context.Books.FindAsync(id);
        if (book is null) throw new BookNotFoundException("Book not found !");
        return book;

    }
    public async Task<Book> GetSingleAsync(Expression<Func<Book, bool>>? expression = null)
    {
        var query = _context.Books.AsQueryable();
        return expression is not null
            ? await query.Where(expression).FirstOrDefaultAsync()
            : await query.FirstOrDefaultAsync();
    }
    public async Task<List<Book>> GetAllAsync(Expression<Func<Book, bool>>? expression = null, params string[] includes)
    {
        var query = _context.Books.AsQueryable();
        query = _getIncludes(query, includes);
        return expression is not null
            ? await query.Where(expression).ToListAsync()
            : await query.ToListAsync();
    }
    public async Task DeleteAsync(int id)
    {
        var existBook = await _context.Books.FindAsync(id);
        if (existBook is null) throw new BookNotFoundException("Book not found!");

        BookImage? posterImage = await _context.BookImages
            .Where(pi => pi.BookId == existBook.Id && pi.IsPoster == true)
            .FirstOrDefaultAsync();
        FileManager.DeleteFile(_env.WebRootPath, "uploads/books", posterImage.ImageUrl);

        BookImage? hoverImage = await _context.BookImages
            .Where(hi => hi.BookId == existBook.Id && hi.IsPoster == false)
            .FirstOrDefaultAsync();
        FileManager.DeleteFile(_env.WebRootPath, "uploads/books", hoverImage.ImageUrl);

        List<BookImage>? bookImages = await _context.BookImages
            .Where(bi => bi.BookId == existBook.Id && bi.IsPoster == null).ToListAsync();
        if (bookImages is not null)
        {
            foreach (var image in bookImages)
            {
                FileManager.DeleteFile(_env.WebRootPath, "uploads/books", image.ImageUrl);
                _context.Remove(image);
                await _context.SaveChangesAsync();
            }
        }
        _context.Books.Remove(existBook);
        await _context.SaveChangesAsync();
    }
    public async Task SoftDeleteAsync(int id)
    {
        throw new NotImplementedException();
    }
    private IQueryable<Book> _getIncludes(IQueryable<Book> query, params string[] includes)
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

    public Task<Book> GetSingleAsync(Expression<Func<Book, bool>>? expression = null, params string[] includes)
    {
        throw new NotImplementedException();
    }
}