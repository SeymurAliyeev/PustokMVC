using PustokMVC.Models;
using PustokMVC.Models;
using System.Linq.Expressions;

namespace Pustok_BookShopMVC.Business.Interfaces;

public interface ISliderService
{
    Task<Slider> GetByIdAsync(int id);
    Task<List<Slider>> GetAllAsync(Expression<Func<Slider, bool>>? expression = null);
    Task CreateAsync(Slider slider);
    Task UpdateAsync(Slider slider);
    Task DeleteAsync(int id);
    Task SoftDeleteAsync(int id);

}
