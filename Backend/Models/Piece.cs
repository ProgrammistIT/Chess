using Chess.Backend.Enums;

namespace Chess.Backend.Models;

public abstract class Piece
{
    public PieceColor Color { get; }
    public PieceType Type { get; }

    protected Piece(PieceColor color, PieceType type)
    {
        Color = color;
        Type = type;
    }
}