using Chess.Model.Core.Enums;

namespace Chess.Model.Data;

public class PieceState
{
    public int Row { get; set; }
    public int Col { get; set; }
    public PieceType Type { get; set; }
    public PieceColor Color { get; set; }
}