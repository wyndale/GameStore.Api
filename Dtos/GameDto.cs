namespace GameStoreAPI.Dtos;

public record GameDto(
    Guid Id,
    string Name,
    string Genre,
    decimal Price,
    DateTime ReleaseDate,
    string ImageUri
    );

public record CreateGameDto(
    string Name,
    string Genre,
    decimal Price,
    DateTime ReleaseDate,
    string ImageUri
    );

public record UpdateGameDto(
    string Name,
    string Genre,
    decimal Price,
    DateTime ReleaseDate,
    string ImageUri
    );