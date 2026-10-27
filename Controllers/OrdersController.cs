using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using para.Models;
using para.Services;

namespace para.Controllers;

[Authorize(Roles = "Admin")]
public class OrdersController(IEntityService<Order> service, IEntityService<User> related) : Controller
{
    public async Task<IActionResult> Index() => View(await service.ListAsync());

    public async Task<IActionResult> Create()
    {
        await PrepareAsync();
        return View();
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Order item)
    {
        ModelState.Remove("User"); ModelState.Remove("OrderItems");
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
        await PrepareAsync(item.UserID);
        return View(item);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Order item)
    {
        ModelState.Remove("User"); ModelState.Remove("OrderItems");
        if (!ModelState.IsValid)
        {
            await PrepareAsync(item.UserID);
            return View(item);
        }
        if (!await service.UpdateAsync(item.OrderID, item)) return NotFound();
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
        ViewData["UserID"] = new SelectList(await related.ListAsync(), "UserID", "Login", selected);
    }
}
