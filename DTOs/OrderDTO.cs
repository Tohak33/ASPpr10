namespace para.DTOs;

public class OrderDTO : IReadDTO
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public string Status { get; set; } = "";
    public UserSummaryDTO User { get; set; } = null!;
    public List<OrderItemDTO> OrderItems { get; set; } = [];
}
