using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace para.DTOs;

public class UpdateProductDTO
{
    [JsonRequired]
    [Range(1, int.MaxValue)]
    public int CategoryId { get; set; }

    [JsonRequired]
    [Required, StringLength(100)]
    public string ProductName { get; set; } = "";

    [JsonRequired]
    [StringLength(500)]
    public string? Description { get; set; }

    [JsonRequired]
    [Range(typeof(decimal), "0.01", "1000000", ParseLimitsInInvariantCulture = true)]
    public decimal Price { get; set; }

    [JsonRequired]
    [Required, StringLength(500)]
    public string ImagePath { get; set; } = "";

    [JsonRequired]
    [Range(0, 1000000)]
    public int StockQuantity { get; set; }
}
