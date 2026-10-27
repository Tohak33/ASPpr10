namespace para.DTOs;

public class CartItemDTO : IReadDTO
{
    public int Id { get; set; }
    public int CartId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal LineTotal { get; set; }
    public ProductDTO Product { get; set; } = null!;
}
