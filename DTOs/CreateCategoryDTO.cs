using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace para.DTOs;

public class CreateCategoryDTO
{
    [JsonRequired]
    [Required, StringLength(100)]
    public string CategoryName { get; set; } = "";
}
