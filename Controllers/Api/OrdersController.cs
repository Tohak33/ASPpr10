using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using para.DTOs;
using para.Services;

namespace para.Controllers.Api;

[Route("api/orders")]
public class OrdersController(IService<OrderDTO, CreateOrderDTO, UpdateOrderDTO, PatchOrderDTO> service)
    : CrudController<OrderDTO, CreateOrderDTO, UpdateOrderDTO, PatchOrderDTO>(service)
{
}
