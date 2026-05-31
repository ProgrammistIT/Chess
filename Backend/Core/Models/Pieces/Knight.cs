using Chess.Backend.Core.Enums;
using Chess.Backend.Models;

namespace Chess.Backend.Core.Models.Pieces;

public class Knight : Piece
{
    public Knight(PieceColor color) : base(color, PieceType.Knight) {}

    public override IEnumerable<(int Row, int Col)> GetValidMoves(Square[,] square, int row, int column)
    {
        (int, int)[] directions = [(1, 2), (1, -2), (-1, 2), (-1, -2), (2, 1), (-2, 1), (2, -1), (-2, -1)];
        return Jump(square, row, column, directions);
    }
}