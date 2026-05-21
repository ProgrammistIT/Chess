using Chess.Backend.Enums;
namespace Chess.Backend.Models.Pieces;

public class Pawn : Piece
{
    public Pawn(PieceColor color) : base(color, PieceType.Pawn) {}
}