using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using para.Models;
using para.Services;

namespace para.Controllers;

[Authorize(Roles = "Admin")]
public class CategoriesController(IEntityService<Category> service) : Controller
{
    public async Task<IActionResult> Index() => View(await service.ListAsync());

    public IActionResult Create() => View();

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Category item)
    {
        if (!ModelState.IsValid)
        {
            return View(item);
        }
        await service.CreateAsync(item);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int? id)
    {
        var item = id.HasValue ? await service.GetAsync(id.Value) : null;
        if (item == null) return NotFound();

        return View(item);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Category item)
    {
        if (!ModelState.IsValid)
        {
            return View(item);
        }
        if (!await service.UpdateAsync(item.CategoryID, item)) return NotFound();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int? id)
    {
        var item = id.HasValue ? await service.GetAsync(id.Value) : null;
        return item == null ? NotFound() : View(item);
    }

    [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        if (!await service.DeleteAsync(id)) return NotFound();
        return RedirectToAction(nameof(Index));
    }
}
