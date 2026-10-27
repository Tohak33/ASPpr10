using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using para.DTOs;
using para.Services;

namespace para.Controllers.Api;

[Route("api/categories")]
public class CategoriesController(IService<CategoryDTO, CreateCategoryDTO, UpdateCategoryDTO, PatchCategoryDTO> service)
    : CrudController<CategoryDTO, CreateCategoryDTO, UpdateCategoryDTO, PatchCategoryDTO>(service)
{
    [AllowAnonymous]
    public override Task<ActionResult<List<CategoryDTO>>> GetAll(int page = 1, int pageSize = 10, CancellationToken ct = default) =>
        base.GetAll(page, pageSize, ct);

    [AllowAnonymous]
    public override Task<ActionResult<CategoryDTO>> GetById(int id, CancellationToken ct = default) => base.GetById(id, ct);

}
