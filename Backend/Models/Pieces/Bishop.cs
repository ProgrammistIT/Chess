using Chess.Backend.Enums;

namespace Chess.Backend.Models.Pieces;

public class Bishop : Piece
{
    public Bishop(PieceColor color) : base(color, PieceType.Bishop) {}
}