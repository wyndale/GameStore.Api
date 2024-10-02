using System.ComponentModel.DataAnnotations;

namespace GameStoreAPI.Entities;

public class Game
{
    public Guid Id { get; set; }

    [Required]
    [StringLength(50)]
    public required string Name { get; set; }

    [Required]
    [StringLength(20)]
    public required string Genre { get; set; }

    [Range(1, 100)]
    public decimal Price { get; set; }

    public DateTime ReleaseDate { get; set; }

    [Url]
    public required string ImageUri { get; set; }
}
 