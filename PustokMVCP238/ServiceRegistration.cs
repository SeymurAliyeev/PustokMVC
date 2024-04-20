using Pustok_BookShopMVC.Business.Implementations;
using PustokMVC.Business.Interfaces;

namespace PustokMVC
{
    public static class ServiceRegistration
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IGenreService, GenreService>();
            services.AddScoped<IBookService, BookService>();
        }
    }
}
