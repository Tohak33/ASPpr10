namespace para.DTOs;

public class UserDTO : IReadDTO
{
    public int Id { get; set; }
    public string Login { get; set; } = "";
    public int RoleId { get; set; }
    public string FullName { get; set; } = "";
    public bool IsActive { get; set; }
    public RoleSummaryDTO Role { get; set; } = null!;
}
