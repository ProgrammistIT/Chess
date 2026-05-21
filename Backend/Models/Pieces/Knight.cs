using Chess.Backend.Enums;

namespace Chess.Backend.Models.Pieces;

public class Knight : Piece
{
    public Knight(PieceColor color) : base(color, PieceType.Knight) {}
}