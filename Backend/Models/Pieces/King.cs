using Chess.Backend.Enums;
namespace Chess.Backend.Models.Pieces;

public class King : Piece
{
    public King (PieceColor color) : base(color, PieceType.King)
    {}
}