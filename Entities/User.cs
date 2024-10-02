using System.ComponentModel.DataAnnotations;

namespace GameStoreAPI.Entities;

public class User
{
    public Guid Id { get; set; }

    [StringLength(30)]
    public required string UserName { get; set; }

    public required byte[] PasswordHash { get; set; }

    public required byte[] PasswordSalt { get; set; }

    public string Role { get; set; } = "User";
}
