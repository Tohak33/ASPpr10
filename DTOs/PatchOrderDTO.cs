using System.ComponentModel.DataAnnotations;

namespace para.DTOs;

public class PatchOrderDTO
{
    [Range(1, int.MaxValue)]
    public int? UserId { get; set; }

    public DateTime? OrderDate { get; set; }

    [Range(typeof(decimal), "0", "100000000000", ParseLimitsInInvariantCulture = true)]
    public decimal? TotalAmount { get; set; }

    [StringLength(50)]
    public string? Status { get; set; }
}
