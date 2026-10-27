namespace para.DTOs;

public class CartDTO : IReadDTO
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public UserSummaryDTO User { get; set; } = null!;
    public List<CartItemDTO> CartItems { get; set; } = [];
}
