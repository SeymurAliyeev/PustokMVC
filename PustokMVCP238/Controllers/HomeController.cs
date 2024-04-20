using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PustokMVC.Business.Interfaces;
using PustokMVC.Data;
using PustokMVC.ViewModels;

namespace PustokMVC.Controllers;

public class HomeController : Controller
{
    private readonly PustokDbContext _context;
    private readonly IGenreService _genreService;
    private readonly IBookService _bookService;

    public HomeController(
            PustokDbContext context, 
            IGenreService genreService,
            IBookService bookService)
    {
        _context = context;
        _genreService = genreService;
        _bookService = bookService;
    }

    public async Task<IActionResult> Index()
    {
        HomeViewModel homeVM = new HomeViewModel()
        {
            Sliders = await _context.Sliders.ToListAsync(),
            BestSellerBooks = await _bookService.GetAllAsync(x=>x.IsBestSeller == true,"BookImages","Author","Genre"),
            FeaturedBooks = await _bookService.GetAllAsync(x=>x.IsFeatured == true,"BookImages","Author","Genre"),
            NewBooks = await _bookService.GetAllAsync(x=>x.IsNew == true,"BookImages","Author","Genre"),
            Genres = await _genreService.GetAllAsync(x=>x.IsDeleted == false),
        };
        return View(homeVM);
    }
}
