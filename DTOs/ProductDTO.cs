namespace para.DTOs;

public class ProductDTO : IReadDTO
{
    public int Id { get; set; }
    public int CategoryId { get; set; }
    public string ProductName { get; set; } = "";
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public string? ImagePath { get; set; }
    public DateTime CreatedAt { get; set; }
    public int StockQuantity { get; set; }
    public CategorySummaryDTO Category { get; set; } = null!;
}
