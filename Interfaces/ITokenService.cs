using GameStoreAPI.Entities;

namespace GameStoreAPI.Interfaces;

public interface ITokenService
{
    string GenerateToken(User user);
}
