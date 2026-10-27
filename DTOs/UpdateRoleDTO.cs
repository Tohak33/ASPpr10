using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace para.DTOs;

public class UpdateRoleDTO
{
    [JsonRequired]
    [Required, StringLength(50)]
    public string RoleName { get; set; } = "";
}
