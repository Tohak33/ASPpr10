using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using para.Models;
using para.Services;

namespace para.Controllers;

[Authorize(Roles = "Admin")]
[Route("Admin")]
public class ProductManagerController(IEntityService<Product> service,
    IEntityService<Category> categories, IImageService images) : Controller
{
    [Route("")]
    [Route("Index")]
    public async Task<IActionResult> Index() => View(await service.ListAsync());

    [HttpGet("Create")]
    public async Task<IActionResult> Create()
    {
        await PrepareAsync();
        return View();
    }

    [HttpPost("Create"), ValidateAntiForgeryToken, RequestSizeLimit(6 * 1024 * 1024)]
    public async Task<IActionResult> Create([Bind("CategoryID,ProductName,Description,Price,StockQuantity")] Product product, IFormFile? uploadedFile)
    {
        if (!ModelState.IsValid)
        {
            await PrepareAsync(product.CategoryID);
            return View(product);
        }
        product.ImagePath = await ImagePathAsync(uploadedFile) ?? "/images/placeholder.png";
        await service.CreateAsync(product);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet("Edit/{id?}")]
    public async Task<IActionResult> Edit(int? id)
    {
        var product = id.HasValue ? await service.GetAsync(id.Value) : null;
        if (product == null) return NotFound();
        await PrepareAsync(product.CategoryID);
        return View(product);
    }

    [HttpPost("Edit/{id?}"), ValidateAntiForgeryToken, RequestSizeLimit(6 * 1024 * 1024)]
    public async Task<IActionResult> Edit(int id,
        [Bind("ProductID,CategoryID,ProductName,Description,Price,ImagePath,StockQuantity")] Product product, IFormFile? uploadedFile)
    {
        if (id != product.ProductID) return NotFound();
        if (!ModelState.IsValid)
        {
            await PrepareAsync(product.CategoryID);
            return View(product);
        }
        product.ImagePath = await ImagePathAsync(uploadedFile) ?? product.ImagePath ?? "/images/placeholder.png";
        if (!await service.UpdateAsync(id, product)) return NotFound();
        return RedirectToAction(nameof(Index));
    }

    [HttpGet("Delete/{id?}")]
    public async Task<IActionResult> Delete(int? id)
    {
        var product = id.HasValue ? await service.GetAsync(id.Value) : null;
        return product == null ? NotFound() : View(product);
    }

    [HttpPost("Delete/{id?}"), ActionName("Delete"), ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        if (!await service.DeleteAsync(id)) return NotFound();
        return RedirectToAction(nameof(Index));
    }

    private async Task PrepareAsync(int? id = null) =>
        ViewData["CategoryID"] = new SelectList(await categories.ListAsync(), "CategoryID", "CategoryName", id);

    private async Task<string?> ImagePathAsync(IFormFile? file)
    {
        if (file == null || file.Length == 0) return null;
        await using var stream = file.OpenReadStream();
        return await images.SaveAsync(stream, file.FileName);
    }
}
