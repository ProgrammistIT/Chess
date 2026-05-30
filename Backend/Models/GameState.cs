namespace Chess.Backend.Models;

public class GameState
{
    public bool IsWhiteTurn { get; set; }
    public List<PieceState> Pieces { get; set; } = new();
}