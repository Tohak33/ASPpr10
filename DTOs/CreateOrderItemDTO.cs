using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace para.DTOs;

public class CreateOrderItemDTO
{
    [JsonRequired]
    [Range(1, int.MaxValue)]
    public int OrderId { get; set; }

    [JsonRequired]
    [Range(1, int.MaxValue)]
    public int ProductId { get; set; }

    [JsonRequired]
    [Range(1, 1000000)]
    public int Quantity { get; set; }

    [JsonRequired]
    [Range(typeof(decimal), "0.01", "1000000", ParseLimitsInInvariantCulture = true)]
    public decimal Price { get; set; }

    [JsonRequired]
    [Range(typeof(decimal), "0.01", "1000000", ParseLimitsInInvariantCulture = true)]
    public decimal PriceAtOrder { get; set; }
}
