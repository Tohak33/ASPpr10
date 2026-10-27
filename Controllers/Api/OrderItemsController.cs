using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using para.DTOs;
using para.Services;

namespace para.Controllers.Api;

[Route("api/order-items")]
public class OrderItemsController(IService<OrderItemDTO, CreateOrderItemDTO, UpdateOrderItemDTO, PatchOrderItemDTO> service)
    : CrudController<OrderItemDTO, CreateOrderItemDTO, UpdateOrderItemDTO, PatchOrderItemDTO>(service)
{
}
