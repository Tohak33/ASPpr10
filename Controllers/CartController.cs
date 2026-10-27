using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using para.Services;

namespace para.Controllers;

[Authorize]
public class CartController(ICartService service) : Controller
{
    private int UserId => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    public async Task<IActionResult> Index() => View(await service.GetItemsAsync(UserId));

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Add(int id)
    {
        await service.AddAsync(UserId, id);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(int id, int qty) =>
        await service.UpdateAsync(UserId, id, qty) ? RedirectToAction(nameof(Index)) : NotFound();

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id) =>
        await service.DeleteAsync(UserId, id) ? RedirectToAction(nameof(Index)) : NotFound();

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Checkout() =>
        RedirectToAction(await service.CheckoutAsync(UserId) ? nameof(OrderSuccess) : nameof(Index));

    public IActionResult OrderSuccess() => View();
}
