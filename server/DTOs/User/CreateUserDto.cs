using System.ComponentModel.DataAnnotations;

namespace Verbum.API.DTOs.User;

public class CreateUserDto {
    [Required] [MinLength(3)] public string Username { get; set; }
    [Required] [MinLength(6)] public string Password { get; set; }
}