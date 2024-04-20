using Microsoft.AspNetCore.Mvc;
using PustokMVC.Business.Interfaces;
using PustokMVC.Data;

namespace PustokMVC.Controllers
{
    public class ShopController : Controller
    {
        private readonly IBookService _bookService;
        private readonly PustokDbContext _context;

        public ShopController(
                IBookService bookService,
                PustokDbContext context)
        {
            _bookService = bookService;
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Detail(int id)
        {
            var book = await _bookService.GetSingleAsync(x => x.Id == id, "BookImages", "Genre", "Author");
            return View(book);
        }
    }
}
