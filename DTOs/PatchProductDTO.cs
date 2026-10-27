using System.ComponentModel.DataAnnotations;

namespace para.DTOs;

public class PatchProductDTO
{
    [Range(1, int.MaxValue)]
    public int? CategoryId { get; set; }

    [StringLength(100)]
    public string? ProductName { get; set; }

    [StringLength(500)]
    public string? Description { get; set; }

    [Range(typeof(decimal), "0.01", "1000000", ParseLimitsInInvariantCulture = true)]
    public decimal? Price { get; set; }

    [StringLength(500)]
    public string? ImagePath { get; set; }

    [Range(0, 1000000)]
    public int? StockQuantity { get; set; }
}
