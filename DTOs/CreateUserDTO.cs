using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace para.DTOs;

public class CreateUserDTO : IPasswordInput
{
    [JsonRequired]
    [Required, StringLength(50, MinimumLength = 3)]
    public string Login { get; set; } = "";

    [JsonRequired]
    [Required, StringLength(100, MinimumLength = 6)]
    public string Password { get; set; } = "";

    [JsonRequired]
    [Range(1, int.MaxValue)]
    public int RoleId { get; set; }

    [JsonRequired]
    [Required, StringLength(100)]
    public string FullName { get; set; } = "";

    [JsonRequired]
    public bool IsActive { get; set; }
}
