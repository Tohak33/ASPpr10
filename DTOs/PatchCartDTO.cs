using System.ComponentModel.DataAnnotations;

namespace para.DTOs;

public class PatchCartDTO
{
    [Range(1, int.MaxValue)]
    public int? UserId { get; set; }
}
