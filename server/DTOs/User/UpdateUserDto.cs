using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Verbum.API.DTOs.User;

public class UpdateUserDto {
    [JsonIgnore] public int Id { get; set; }
    [Required] [MinLength(3)] public string Username { get; set; } = string.Empty;
    [Required] [MinLength(6)] public string Password { get; set; } = string.Empty;
}