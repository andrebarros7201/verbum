namespace Verbum.API.DTOs.User;

public class MemberDto {
    public int Id { get; set; }
    public string Username { get; set; } = String.Empty;
    public bool isAdmin { get; set; }
}
