using System.ComponentModel.DataAnnotations;

namespace para.Models;

public class ProductReservation
{
    public int ProductReservationID { get; set; }
    public int ProductID { get; set; }
    public int UserID { get; set; }
    public int Quantity { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? CancelledAt { get; set; }
    [Timestamp]
    public byte[] RowVersion { get; set; } = [];
    public Product Product { get; set; } = null!;
    public User User { get; set; } = null!;
}
