using Microsoft.AspNetCore.Mvc;
using PustokMVC.Business.Interfaces;
using PustokMVC.CustomExceptions.GenreExceptions;
using PustokMVC.Models;


namespace Pustok_BookShopMVC.Areas.Admin.Controllers;
[Area("Admin")]
public class GenreController : Controller
{
    private readonly IGenreService _genreService;

    public GenreController(IGenreService genreService)
    {
        _genreService = genreService;
    }
    public async Task<IActionResult> Index()
        => View(await _genreService.GetAllAsync(x => x.IsDeleted == false, "Books"));
    public IActionResult Create()
        => View();
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Genre genre)
    {
        if (!ModelState.IsValid) return View();
        try
        {
            await _genreService.CreateAsync(genre);
        }
        catch (GenreNameAlreadyExistException ex)
        {
            ModelState.AddModelError(ex.PropertyName, ex.Message);
            return View();
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return View();
        }
        return RedirectToAction("Index");
    }
    public async Task<IActionResult> Update(int id)
    {
        Genre genre = null;
        try
        {
            genre = await _genreService.GetByIdAsync(id);
        }
        catch (GenreNotFoundException ex)
        {
            return View(ex.Message);
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return View();
        }

        return View(genre);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(Genre genre)
    {
        if (!ModelState.IsValid) return View();
        try
        {
            await _genreService.UpdateAsync(genre);
        }
        catch (GenreNameAlreadyExistException ex)
        {
            ModelState.AddModelError(ex.PropertyName, ex.Message); return View();
        }
        catch (GenreNotFoundException ex)
        {
            return View("Error");
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return View();
        }
        return RedirectToAction("Index");
    }
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _genreService.DeleteAsync(id);
        }
        catch (GenreNotFoundException ex)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            return NotFound();
        }
        return Ok();
    }
}