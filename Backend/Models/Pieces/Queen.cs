using Chess.Backend.Enums;
namespace Chess.Backend.Models.Pieces;

public class Queen : Piece
{
    public Queen(PieceColor color) : base(color, PieceType.Queen) {}
}