namespace para.DTOs;

public class ProductSummaryDTO
{
    public int Id { get; set; }
    public string ProductName { get; set; } = "";
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
}
