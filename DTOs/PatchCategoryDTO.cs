using System.ComponentModel.DataAnnotations;

namespace para.DTOs;

public class PatchCategoryDTO
{
    [StringLength(100)]
    public string? CategoryName { get; set; }
}
