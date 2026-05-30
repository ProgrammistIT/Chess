using Chess.Backend.Enums;

namespace Chess.Backend.Models;

public class PieceState
{
    public int Row { get; set; }
    public int Col { get; set; }
    public PieceType Type { get; set; }
    public PieceColor Color { get; set; }
}