using System.ComponentModel.DataAnnotations;

namespace para.DTOs;

public class PatchRoleDTO
{
    [StringLength(50)]
    public string? RoleName { get; set; }
}
