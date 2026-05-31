using Chess.Backend.Core.Enums;
using Chess.Backend.Models;

namespace Chess.Backend.Core.Models.Pieces;

public class Rook : Piece
{
    public Rook(PieceColor color) : base(color, PieceType.Rook) {}
    public override IEnumerable<(int Row, int Col)> GetValidMoves(Square[,] square, int row, int column)
    {
        foreach (var (dr, dc) in new[] {(-1, 0), (1, 0), (0, -1), (0, 1) })
        {
            foreach (var move in Slide(square, row, column, dr, dc))
                yield return move;
        }
    }
}
