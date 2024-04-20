using PustokMVC.Models;
using System.Linq.Expressions;

namespace Pustok_BookShopMVC.Business.Interfaces;

public interface IAuthorService
{
    Task<Author> GetByIdAsync(int id);
    Task<Author> GetSingleAsync(Expression<Func<Author, bool>>? expression = null);
    Task<List<Author>> GetAllAsync(Expression<Func<Author, bool>>? expression = null, params string[] includes);
    Task CreateAsync(Author author);
    Task UpdateAsync(Author author);
    Task DeleteAsync(int id);
    Task SoftDeleteAsync(int id);
}