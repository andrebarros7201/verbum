using System.ComponentModel.DataAnnotations;

namespace Verbum.API.DTOs.User;

public class UpdateUserDto {
    [Required] [MinLength(3)] public string Username { get; set; } = string.Empty;
    [Required] [MinLength(6)] public string Password { get; set; } = string.Empty;
}