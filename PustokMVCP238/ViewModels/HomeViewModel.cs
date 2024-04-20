using PustokMVC.Models;

namespace PustokMVC.ViewModels;

public class HomeViewModel
{
    public List<Slider> Sliders { get; set; }
    public List<Genre> Genres { get; set; }
    public List<Book> FeaturedBooks { get; set; }
    public List<Book> NewBooks { get; set; }
    public List<Book> BestSellerBooks { get; set; }
}
