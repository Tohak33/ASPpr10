namespace para.DTOs;

public class RoleDTO : IReadDTO
{
    public int Id { get; set; }
    public string RoleName { get; set; } = "";
    public List<UserSummaryDTO> Users { get; set; } = [];
}
