using Chess.Backend.Enums;

namespace Chess.Backend.Models.Pieces;

public class Rook : Piece
{
    public Rook(PieceColor color) : base(color, PieceType.Rook) {}
}