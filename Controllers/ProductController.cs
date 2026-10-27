using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using para.Services;

namespace para.Controllers;

public class ProductController(ICatalogService service) : Controller
{
    public async Task<IActionResult> Index(string? searchString, int? categoryId, string? sortOrder, int pageNumber = 1)
    {
        var result = await service.GetAsync(searchString, categoryId, sortOrder, pageNumber);
        ViewBag.PageNumber = result.PageNumber;
        ViewBag.TotalPages = result.TotalPages;
        ViewBag.Categories = new SelectList(result.Categories, "CategoryID", "CategoryName", categoryId);
        return View(result.Items);
    }
}
