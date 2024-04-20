using PustokMVC.Models;
using System.Linq.Expressions;

namespace PustokMVC.Business.Interfaces
{
    public interface IBookService
    {
        Task<Book> GetByIdAsync(int id);
        Task<Book> GetSingleAsync(Expression<Func<Book, bool>>? expression = null, params string[] includes);
        Task<List<Book>> GetAllAsync(Expression<Func<Book, bool>>? expression = null, params string[] includes);
        Task CreateAsync(Book Book);
        Task UpdateAsync(Book Book);
        Task DeleteAsync(int id);
        Task SoftDeleteAsync(int id);
    }
}
