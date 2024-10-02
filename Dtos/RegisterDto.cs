using System.ComponentModel.DataAnnotations;

namespace GameStoreAPI.Dtos;

public record RegisterDto(
    [Required] [StringLength(25)]
    string Username,
    [Required]
    string Password
    );
