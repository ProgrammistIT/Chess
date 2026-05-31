using Chess.Model.Core.Enums;
using Chess.Model.Models;

namespace Chess.Model.Core.Models.Pieces;

public class King : Piece
{
    public King (PieceColor color) : base(color, PieceType.King) {}

    public override IEnumerable<(int Row, int Col)> GetValidMoves(Square[,] square, int row, int column)
    {
        (int, int)[] directions = [(0, 1), (0, -1), (1, 0), (-1, 0), (1, 1), (-1, -1), (-1, 1), (1, -1)];
        return Jump(square, row, column, directions);
    }
}