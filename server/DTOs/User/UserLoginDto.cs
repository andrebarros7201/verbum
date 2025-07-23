using System.ComponentModel.DataAnnotations;

namespace Verbum.API.DTOs.User;

public class UserLoginDto {
    [Required] public string Username { get; set; } = string.Empty;
    [Required] public string Password { get; set; } = string.Empty;
}