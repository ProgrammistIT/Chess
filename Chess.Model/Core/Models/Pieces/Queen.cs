using Chess.Model.Core.Enums;
using Chess.Model.Models;

namespace Chess.Model.Core.Models.Pieces;

public class Queen : Piece
{
    public Queen(PieceColor color) : base(color, PieceType.Queen) {}

    public override IEnumerable<(int Row, int Col)> GetValidMoves(Square[,] square, int row, int column)
    {
        foreach (var (dr, dc) in new[] { (1, 0), (0, 1), (-1, 0), (0, -1), (-1, -1), (-1, 1), (1, 1), (1, -1) })
        {
            foreach (var move in Slide(square, row, column, dr, dc))
                yield return move;
        }
    }
}
