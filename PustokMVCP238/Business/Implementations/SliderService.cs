using Microsoft.EntityFrameworkCore;
using Pustok_BookShopMVC.Business.Interfaces;
using PustokMVC.CustomExceptions.GenreExceptions;
using PustokMVC.CustomExceptions.SliderExceptions;
using PustokMVC.Extensions;
using PustokMVC.Models;
using System.Linq.Expressions;
using PustokMVC.Data;

namespace Pustok_BookShopMVC.Business.Implementations;

public class SliderService : ISliderService
{
    private readonly PustokDbContext _context;
    private readonly IWebHostEnvironment _env;
    public SliderService(PustokDbContext context, IWebHostEnvironment env)
    {
        _context = context;
        _env = env;
    }
    public async Task CreateAsync(Slider slider)
    {
        slider.CreatedDate = DateTime.UtcNow.AddHours(4);
        slider.ModifiedDate = DateTime.UtcNow.AddHours(4);
        await _context.Sliders.AddAsync(slider);
    }
    public async Task UpdateAsync(Slider slider)
    {
        var existSlider = await _context.Sliders.FindAsync(slider.Id);
        if (existSlider is null) throw new SliderNotFoundException("Slider not found!");
        existSlider.Title1 = slider.Title1;
        existSlider.Title2 = slider.Title2;
        existSlider.Desc = slider.Desc;
        existSlider.RedirectUrlText = slider.RedirectUrlText;
        existSlider.RedirectUrl = slider.RedirectUrl;
        existSlider.ModifiedDate = DateTime.UtcNow.AddHours(4);
        await _context.SaveChangesAsync();
    }
    public async Task<Slider> GetByIdAsync(int id)
    {
        var slider = await _context.Sliders.FindAsync(id);
        if (slider is null) throw new SliderNotFoundException("Slider not found!");
        return slider;
    }
    public async Task<List<Slider>> GetAllAsync(Expression<Func<Slider, bool>>? expression = null)
    {
        var query = _context.Sliders.AsQueryable();
        return expression is not null
            ? await query.Where(expression).ToListAsync()
            : await query.ToListAsync();
    }
    public async Task DeleteAsync(int id)
    {
        var existSlider = await _context.Sliders.FindAsync(id);
        if (existSlider is null) throw new SliderNotFoundException("Slider not found!");

        FileManager.DeleteFile(_env.WebRootPath, "uploads/sliders", existSlider.ImageUrl);

        _context.Remove(existSlider);
        await _context.SaveChangesAsync();
    }
    public async Task SoftDeleteAsync(int id)
    {
        var slider = await _context.Sliders.FindAsync(id);
        if (slider is null) throw new GenreNotFoundException("Slider not found!");
        slider.ModifiedDate = DateTime.UtcNow.AddHours(4);
        slider.IsDeleted = !slider.IsDeleted;
    }
}