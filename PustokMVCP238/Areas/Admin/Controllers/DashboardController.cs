using Microsoft.AspNetCore.Mvc;

namespace Pustok_BookShopMVC.Areas.Admin.Controllers;
[Area("Admin")]
public class DashboardController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}