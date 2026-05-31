namespace Chess.Model.Data;

public class GameState
{
    public bool IsWhiteTurn { get; set; }
    public List<PieceState> Pieces { get; set; } = new();
}