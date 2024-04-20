using Microsoft.AspNetCore.Mvc;
using Pustok_BookShopMVC.Business.Interfaces;
using PustokMVC.CustomExceptions.AuthorExceptions;
using PustokMVC.Models;
namespace PustokMVC.Areas.Admin.Controllers;

[Area("Admin")]
public class AuthorController : Controller
{
    private readonly IAuthorService _authorService;

    public AuthorController(IAuthorService authorService)
    {
        _authorService = authorService;
    }
    public async Task<IActionResult> Index()
       => View(await _authorService.GetAllAsync(x => x.IsDeleted == false, "Books"));
    public IActionResult Create()
        => View();
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Author author)
    {
        if (!ModelState.IsValid) return View();
        try
        {
            await _authorService.CreateAsync(author);
        }
        catch (AuthorNameAlreadyExistException ex)
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
        Author author = null;
        try
        {
            await _authorService.GetByIdAsync(id);
        }
        catch (AuthorNotFoundException ex)
        {
            return View(ex.Message);
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return View();
        }
        return View(author);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(Author author)
    {
        if (!ModelState.IsValid) return View();
        try
        {
            await _authorService.UpdateAsync(author);
        }
        catch (AuthorNameAlreadyExistException ex)
        {
            ModelState.AddModelError(ex.PropertyName, ex.Message);
            return View();
        }
        catch (AuthorNotFoundException ex)
        {
            ModelState.AddModelError("", ex.Message);
        }
        return RedirectToAction("Index");
    }
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _authorService.DeleteAsync(id);
        }
        catch (AuthorNotFoundException ex)
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
