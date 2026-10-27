using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using para.Models;
using para.Services;

namespace para.Controllers;

[Authorize(Roles = "Admin")]
public class UsersController(IEntityService<User> service, IEntityService<Role> related) : Controller
{
    public async Task<IActionResult> Index() => View(await service.ListAsync());

    public async Task<IActionResult> Create()
    {
        await PrepareAsync();
        return View();
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(User item)
    {
        if (!ModelState.IsValid)
        {
            await PrepareAsync();
            return View(item);
        }
        await service.CreateAsync(item);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int? id)
    {
        var item = id.HasValue ? await service.GetAsync(id.Value) : null;
        if (item == null) return NotFound();
        await PrepareAsync(item.RoleID);
        return View(item);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(User item)
    {
        if (!ModelState.IsValid)
        {
            await PrepareAsync(item.RoleID);
            return View(item);
        }
        if (!await service.UpdateAsync(item.UserID, item)) return NotFound();
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
    private async Task PrepareAsync(int? selected = null)
    {
        ViewData["RoleID"] = new SelectList(await related.ListAsync(), "RoleID", "RoleName", selected);
    }
}
