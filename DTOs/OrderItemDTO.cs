namespace para.DTOs;

public class OrderItemDTO : IReadDTO
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal PriceAtOrder { get; set; }
    public ProductDTO Product { get; set; } = null!;
}
