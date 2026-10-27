namespace para.DTOs;

public class CategoryDTO : IReadDTO
{
    public int Id { get; set; }
    public string CategoryName { get; set; } = "";
    public List<ProductSummaryDTO> Products { get; set; } = [];
}
