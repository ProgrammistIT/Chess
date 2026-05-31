using Chess.Backend.Models;

namespace Chess.Backend.Data;

public interface ISerialize
{
    void Serialize(Board board);
    GameState? Deserialize();
}