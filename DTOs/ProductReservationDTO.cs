namespace para.DTOs;

public class ProductReservationDTO : IReadDTO
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? CancelledAt { get; set; }
    public ProductDTO Product { get; set; } = null!;
    public UserSummaryDTO User { get; set; } = null!;
}
