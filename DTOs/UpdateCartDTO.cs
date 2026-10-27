using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace para.DTOs;

public class UpdateCartDTO
{
    [JsonRequired]
    [Range(1, int.MaxValue)]
    public int UserId { get; set; }
}
