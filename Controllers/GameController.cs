using GameStoreAPI.Data;
using GameStoreAPI.Dtos;
using GameStoreAPI.Entities;
using GameStoreAPI.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameStoreAPI.Controllers;

public class GameController : BaseController
{
    private readonly DataContext _context;

    public GameController(DataContext context)
    {
        _context = context;
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<GameDto>>> GetAllGames()
    {
        var games = await _context.Games.ToListAsync();

        var gameDto = games.Select(game => game.AsDto()).ToList();

        return Ok(gameDto);
    }

    [Authorize]
    [HttpGet("{id}")]
    public async Task<ActionResult<GameDto>> GetGameById(Guid id)
    {
        var games = await _context.Games.FindAsync(id);

        if (games == null)
        {
            return NotFound();
        }

        return Ok(games.AsDto());
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<ActionResult<GameDto>> CreateGame(CreateGameDto createGameDto)
    {
        var game = new Game
        {
            Id = Guid.NewGuid(),
            Name = createGameDto.Name,
            Genre = createGameDto.Genre,
            Price = createGameDto.Price,
            ReleaseDate = createGameDto.ReleaseDate,
            ImageUri = createGameDto.ImageUri
        };

        _context.Games.Add(game);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(CreateGame), new { id = game.Id }, game.AsDto());
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateGame(UpdateGameDto updateGame, Guid id)
    {
        var game = await _context.Games.FindAsync(id);

        if (updateGame == null)
        {
            return NotFound();
        }

        game!.Name = updateGame.Name;
        game.Genre = updateGame.Genre;
        game.Price = updateGame.Price;
        game.ReleaseDate = updateGame.ReleaseDate;
        game.ImageUri = updateGame.ImageUri;

        _context.Entry(game).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!GameExist(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<ActionResult<Game>> DeleteGame(Guid id)
    {
        var game = await _context.Games.FindAsync(id);

        if (game == null)
        {
            return NoContent();
        }

        _context.Games.Remove(game);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool GameExist(Guid id)
    {
        return _context.Games.Any(game =>  game.Id == id);
    }
}
