using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using para.DTOs;
using para.Services;

namespace para.Controllers.Api;

[ApiController]
[Authorize]
[Route("api/reservations")]
public class ProductReservationsController(IReservationService service) : ControllerBase
{
    private int UserId => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!, CultureInfo.InvariantCulture);

    [HttpGet]
    public async Task<ActionResult<List<ProductReservationDTO>>> GetAll([Range(1, int.MaxValue)] int page = 1,
        [Range(1, 100)] int pageSize = 10, CancellationToken ct = default)
    {
        var result = await service.GetAllAsync(UserId, page, pageSize, ct);
        Response.Headers["X-Total-Count"] = result.TotalCount.ToString(CultureInfo.InvariantCulture);
        return Ok(result.Items);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ProductReservationDTO>> GetById(int id, CancellationToken ct = default)
    {
        var item = await service.GetByIdAsync(UserId, id, ct);
        return item == null ? NotFound() : Ok(item);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<ProductReservationDTO>> Reserve(CreateProductReservationDTO dto, CancellationToken ct = default)
    {
        var item = await service.ReserveAsync(UserId, dto, ct);
        return CreatedAtAction(nameof(GetById), new { id = item.Id }, item);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Update(int id, UpdateProductReservationDTO dto, CancellationToken ct = default) =>
        await service.UpdateAsync(UserId, id, dto, ct) ? NoContent() : NotFound();

    [HttpPatch("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Patch(int id, PatchProductReservationDTO dto, CancellationToken ct = default) =>
        await service.PatchAsync(UserId, id, dto, ct) ? NoContent() : NotFound();

    [HttpPost("{id:int}/cancel")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Cancel(int id, CancellationToken ct = default) =>
        await service.CancelAsync(UserId, id, ct) ? NoContent() : NotFound();

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Delete(int id, CancellationToken ct = default) =>
        await service.DeleteAsync(UserId, id, ct) ? NoContent() : NotFound();
}
