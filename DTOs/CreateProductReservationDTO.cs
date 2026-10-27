using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace para.DTOs;

public class CreateProductReservationDTO
{
    [JsonRequired, Range(1, int.MaxValue)]
    public int ProductId { get; set; }
    [JsonRequired, Range(1, 1000000)]
    public int Quantity { get; set; }
}
