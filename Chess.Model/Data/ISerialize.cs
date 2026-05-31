using Chess.Model.Models;

namespace Chess.Model.Data;

public interface ISerialize
{
    void Serialize(Board board);
    GameState? Deserialize();
}