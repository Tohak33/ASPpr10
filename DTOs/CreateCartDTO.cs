using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace para.DTOs;

public class CreateCartDTO
{
    [JsonRequired]
    [Range(1, int.MaxValue)]
    public int UserId { get; set; }
}
