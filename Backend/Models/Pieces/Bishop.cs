using Chess.Backend.Enums;

namespace Chess.Backend.Models.Pieces;

public class Bishop : Piece
{
    public Bishop(PieceColor color) : base(color,  PieceType.Bishop) {}

    public override IEnumerable<(int Row, int Col)> GetValidMoves(Square[,] square, int row, int column)
    {
        foreach (var (dr, dc) in new[] {(1,1), (1,-1), (-1, 1), (-1, -1)})
        {
            foreach (var move in Slide(square, row, column, dr, dc))
                yield return move;
        }
    }
}