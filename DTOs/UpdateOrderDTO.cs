using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace para.DTOs;

public class UpdateOrderDTO
{
    [JsonRequired]
    [Range(1, int.MaxValue)]
    public int UserId { get; set; }

    [JsonRequired]
    public DateTime OrderDate { get; set; }

    [JsonRequired]
    [Range(typeof(decimal), "0", "100000000000", ParseLimitsInInvariantCulture = true)]
    public decimal TotalAmount { get; set; }

    [JsonRequired]
    [Required, StringLength(50)]
    public string Status { get; set; } = "";
}
