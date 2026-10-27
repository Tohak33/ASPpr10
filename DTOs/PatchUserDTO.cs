using System.ComponentModel.DataAnnotations;

namespace para.DTOs;

public class PatchUserDTO : IPasswordInput
{
    [StringLength(50, MinimumLength = 3)]
    public string? Login { get; set; }

    [StringLength(100, MinimumLength = 6)]
    public string? Password { get; set; }

    [Range(1, int.MaxValue)]
    public int? RoleId { get; set; }

    [StringLength(100)]
    public string? FullName { get; set; }

    public bool? IsActive { get; set; }
}
