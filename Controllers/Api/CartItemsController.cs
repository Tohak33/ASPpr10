using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using para.DTOs;
using para.Services;

namespace para.Controllers.Api;

[Route("api/cart-items")]
public class CartItemsController(IService<CartItemDTO, CreateCartItemDTO, UpdateCartItemDTO, PatchCartItemDTO> service)
    : CrudController<CartItemDTO, CreateCartItemDTO, UpdateCartItemDTO, PatchCartItemDTO>(service)
{
}
