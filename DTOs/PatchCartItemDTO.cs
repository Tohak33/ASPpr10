using System.ComponentModel.DataAnnotations;

namespace para.DTOs;

public class PatchCartItemDTO
{
    [Range(1, int.MaxValue)]
    public int? CartId { get; set; }

    [Range(1, int.MaxValue)]
    public int? ProductId { get; set; }

    [Range(1, 1000000)]
    public int? Quantity { get; set; }
}
