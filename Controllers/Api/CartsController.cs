using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using para.DTOs;
using para.Services;

namespace para.Controllers.Api;

[Route("api/carts")]
public class CartsController(IService<CartDTO, CreateCartDTO, UpdateCartDTO, PatchCartDTO> service)
    : CrudController<CartDTO, CreateCartDTO, UpdateCartDTO, PatchCartDTO>(service)
{
}
