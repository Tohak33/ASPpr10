using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using para.DTOs;
using para.Services;

namespace para.Controllers.Api;

[Route("api/products")]
public class ProductsController(IService<ProductDTO, CreateProductDTO, UpdateProductDTO, PatchProductDTO> service)
    : CrudController<ProductDTO, CreateProductDTO, UpdateProductDTO, PatchProductDTO>(service)
{
    [AllowAnonymous]
    public override Task<ActionResult<List<ProductDTO>>> GetAll(int page = 1, int pageSize = 10, CancellationToken ct = default) =>
        base.GetAll(page, pageSize, ct);

    [AllowAnonymous]
    public override Task<ActionResult<ProductDTO>> GetById(int id, CancellationToken ct = default) => base.GetById(id, ct);

}
